using Application.DTO;
using Application.Services;
using Domain.Entities;

namespace BookStreetAPI.Controllers
{
    public class BookController : BaseController<Book, BookDTO, IBookService>
    {
        public BookController(IBookService service) : base(service)
        {
        }
    }
}
