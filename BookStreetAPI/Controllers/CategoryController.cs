using Application.DTO;
using Application.Services;
using Domain.Entities;

namespace BookStreetAPI.Controllers
{
    public class CategoryController : BaseController<Category, CategoryDTO, ICategoryService>
    {
        public CategoryController(ICategoryService service) : base(service)
        {
        }
    }
}
