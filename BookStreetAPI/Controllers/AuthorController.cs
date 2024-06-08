using Application.DTO;
using Application.Services;
using Domain.Entities;

namespace BookStreetAPI.Controllers
{
    public class AuthorController : BaseController<Author, AuthorDTO, IAuthorService>
    {
        public AuthorController(IAuthorService service) : base(service)
        {
        }
    }
}
