using Application.DTO;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public User? Login(LoginDTO req);
    }
}
