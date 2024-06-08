using Application.DTO;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Mappers
{
    /// <summary>
    /// This class is used to map objects.
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            UserMapper();
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<DistributorDTO, Distributor>().ReverseMap();
            CreateMap<AuthorDTO, Author>().ReverseMap();
            CreateMap<EventDTO, Event>().ReverseMap();
            CreateMap<PublisherDTO, Publisher>().ReverseMap();
            CreateMap<GenreDTO, Genre>().ReverseMap();
            CreateMap<BookDTO, Book>().ReverseMap();    
        }

        private void UserMapper()
        {
            CreateMap<User, UserDTO>().ForMember(dest => dest.Password, opt => opt.Ignore());
            CreateMap<UserDTO, User>();
        }
    }
}