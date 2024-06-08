using System.Net;

namespace Domain.Constants
{
    /// <summary>
    /// 
    /// </summary>
    public enum ResponseStatus
    {
        Success = HttpStatusCode.OK,
        BadRequest = HttpStatusCode.BadRequest,
        Unauthorized = HttpStatusCode.Unauthorized,
        Forbidden = HttpStatusCode.Forbidden,
        NotFound = HttpStatusCode.NotFound,
        InternalServerError = HttpStatusCode.InternalServerError,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ActionRow
    {
        ADD = 1,
        REMOVE = 2,
        UPDATE = 3
    }
}
