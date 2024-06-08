using Application.DTO;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Services
{
    public class UserService : BaseService<User, UserDTO>, IUserService
    {
        private IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public UserService(IBaseRepository<User> repository, IMapper mapper, BookStreetContext context, IUserRepository userRepository, IJwtService jwtService) : base(repository, mapper, context)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public ServiceResponse Login(LoginDTO req)
        {
            ServiceResponse serviceResponse = new();
            var user = _userRepository.Login(req);
            if (user == null)
            {
                return serviceResponse.OnError("Username or password is incorrect");
            }
            var userDTO = _mapper.Map<UserDTO>(user);
            var token = _jwtService.GenerateUserToken(userDTO);
            return serviceResponse.Onsuccess(token);
        }
    }
}
