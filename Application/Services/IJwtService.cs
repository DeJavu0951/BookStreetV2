
using Application.DTO;

namespace Application.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IJwtService
    {
        public UserLoginResponse GenerateUserToken(UserDTO user);
    }
}
