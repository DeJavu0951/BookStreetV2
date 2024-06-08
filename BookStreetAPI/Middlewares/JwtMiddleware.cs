
using Application.DTO;
using System.IdentityModel.Tokens.Jwt;

namespace BookStreetAPI.Middlewares
{
    public class JwtMiddleware : IMiddleware
    {
        public JwtMiddleware()
        {
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await Extract(context);
            await next(context);
        }
        private async Task Extract(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer", "").Trim();
            if (!string.IsNullOrEmpty(token))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);

                var services = context.RequestServices;
                var session = services.GetService(typeof(SessionContext)) as SessionContext ?? throw new Exception("SessionContext is not injection");
            }
        }
    }
}
