using Application.DTO;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace BookStreetAPI.Middlewares
{
    /// <summary>
    /// Middleware xử lý exception của ứng dụng
    /// </summary>
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(
                $"An error occurred while processing your request: {exception.Message}");

            var serviceResponse = new ServiceResponse
            {
                Message = exception.Message,
                Success = false
            };

            switch (exception)
            {
                case BadHttpRequestException:
                    serviceResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    serviceResponse.Message = exception.GetType().Name;
                    break;

                default:
                    serviceResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    serviceResponse.Message = "Internal Server Error";
                    break;
            }

            httpContext.Response.StatusCode = serviceResponse.StatusCode;

            await httpContext
                .Response
                .WriteAsJsonAsync(serviceResponse, cancellationToken);

            return true;
        }
    }
}
