using Application.DTO;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStreetAPI.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseController<User, UserDTO, IUserService>
    {
        public AuthController(IUserService service) : base(service)
        {
        }

        [HttpPost("Login")]
        public ServiceResponse Login(LoginDTO req)
        {
            return _service.Login(req);
        }
    }
}
