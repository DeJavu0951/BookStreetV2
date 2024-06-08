using Application.DTO;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Services
{
    public class GenreService : BaseService<Genre, GenreDTO>, IGenreService
    {
        public GenreService(IBaseRepository<Genre> repository, IMapper mapper, BookStreetContext context) : base(repository, mapper, context)
        {
        }
    }
}
