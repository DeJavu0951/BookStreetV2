using Application.DTO;
using Domain.Entities;

namespace Application.Services
{
    public interface ICategoryService: IBaseService<Category, CategoryDTO>
    {
    }
}
