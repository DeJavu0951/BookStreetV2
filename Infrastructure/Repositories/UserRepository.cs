using Application.DTO;
using Application.Repositories;
using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(BookStreetContext context) : base(context)
        {
        }

        public User? Login(LoginDTO req)
        {
            return _context.Users.FirstOrDefault(user => user.Username.ToLower() == req.Username.ToLower() && user.Password == req.Password);
        }
    }
}
