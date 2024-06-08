using Application.DTO;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Services
{
    public class BookService : BaseService<Book, BookDTO>, IBookService
    {
        public BookService(IBaseRepository<Book> repository, IMapper mapper, BookStreetContext context) : base(repository, mapper, context)
        {
        }
    }
}
