using Application.DTO;
using Domain.Entities;

namespace Application.Services
{
    public interface IBookService: IBaseService<Book, BookDTO>
    {
    }
}
