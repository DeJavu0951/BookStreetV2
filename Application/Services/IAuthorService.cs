using Application.DTO;
using Domain.Entities;

namespace Application.Services
{
    public interface IAuthorService: IBaseService<Author, AuthorDTO>
    {
    }
}
