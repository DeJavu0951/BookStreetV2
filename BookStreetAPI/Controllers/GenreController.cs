using Application.DTO;
using Application.Services;
using Domain.Entities;

namespace BookStreetAPI.Controllers
{
    public class GenreController : BaseController<Genre, GenreDTO, IGenreService>
    {
        public GenreController(IGenreService service) : base(service)
        {
        }
    }
}
