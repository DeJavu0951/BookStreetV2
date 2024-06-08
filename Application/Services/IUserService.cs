using Application.DTO;
using Domain.Entities;

namespace Application.Services
{
    public interface IUserService : IBaseService<User, UserDTO>
    {
        public ServiceResponse Login(LoginDTO req);
    }
}
