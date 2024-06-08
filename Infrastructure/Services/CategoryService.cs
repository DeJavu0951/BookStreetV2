using Application.DTO;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Services
{
    public class CategoryService : BaseService<Category, CategoryDTO>, ICategoryService
    {
        public CategoryService(IBaseRepository<Category> repository, IMapper mapper, BookStreetContext context) : base(repository, mapper, context)
        {
        }
    }
}
