using Application.DTO;

namespace Application.Services
{
    public interface IBaseService<TEntity, TDto>
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(int id);
        Task<ServiceResponse> AddAsync(TDto dto);
        Task<ServiceResponse> UpdateAsync(int id, TDto dto);
        Task<ServiceResponse> DeleteAsync(int id);

        TDto MapToDto(TEntity entity);
        void MapToEntity(TDto dto, TEntity entity);
    }
}
