using Application.DTO;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Services
{
    public class AuthorService : BaseService<Author, AuthorDTO>, IAuthorService
    {
        public AuthorService(IBaseRepository<Author> repository, IMapper mapper, BookStreetContext context) : base(repository, mapper, context)
        {
        }
    }
}
