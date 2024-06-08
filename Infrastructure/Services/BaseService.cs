using Application.DTO;
using Application.Extentions;
using Application.Repositories;
using AutoMapper;
using Domain.Attribute;
using Infrastructure.Context;
using System.ComponentModel.DataAnnotations;

namespace Application.Services
{
    public class BaseService<TEntity, TDto> : IBaseService<TEntity, TDto> where TEntity : class
    {
        public readonly IBaseRepository<TEntity> _repository;
        public IMapper _mapper;
        public BookStreetContext _context;
        public BaseService(IBaseRepository<TEntity> repository, IMapper mapper, BookStreetContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var results = await _repository.GetAllAsync();
            var mappingResults = _mapper.Map<IEnumerable<TDto>>(results);
            return mappingResults;
        }

        public async Task<TDto> GetByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Entity not found");
            return _mapper.Map<TDto>(result);
        }

        public async Task<ServiceResponse> AddAsync(TDto dto)
        {
            ServiceResponse response = new ServiceResponse();
            response.Success = true;

            var entity = Activator.CreateInstance<TEntity>();
            MapToEntity(dto, entity);
            response = await ValidateCreateAsync(entity);
            if (!response.Success) return response;

            response.Data = await _repository.AddAsync(entity);
            return response;
        }

        public async Task<ServiceResponse> UpdateAsync(int id, TDto dto)
        {
            ServiceResponse response = new ServiceResponse();
            response.Success = true;

            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                response.Success = false;
                return response;
            }

            MapToEntity(dto, entity);

            response = await ValidateUpdateAsync(id, entity);
            if (!response.Success) return response;
            response.Data = await _repository.UpdateAsync(entity);
            return response;
        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            ServiceResponse response = new ServiceResponse();
            response.Success = true;

            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                response.Success = false;
                return response;
            }

            response = await ValidateDeleteAsync(entity);
            if (!response.Success) return response;

            response.Data = await _repository.DeleteAsync(id);
            return response;
        }

        public virtual Task<ServiceResponse> ValidateCreateAsync(TEntity entity)
        {
            // tên của các property cần check trùng
            var properties = entity.GetType().GetCustomAttributes(typeof(ValidateDuplicateAttribute), true);
            if (properties.Length == 0) return Task.FromResult(new ServiceResponse { Success = true });

            // tạo một mảng query để kiểm tra trùng
            IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable();
            foreach (var property in properties)
            {
                var propertyInfo = entity.GetType().GetProperty(((ValidateDuplicateAttribute)property).GetName());
                var value = propertyInfo.GetValue(entity);

                // Lấy biểu thức tương ứng với điều kiện tìm kiếm
                var exp = ExpressionBuilder.GetExpression<TEntity>(new FilterDefinition { Operand = Operand.Equal, Field = propertyInfo.Name, Value = value?.ToString() });
                if (exp != null) query = query.Where(exp);
            }

            // kiểm tra xem có bản ghi nào trùng không
            if (query != null && query.Any())
            {
                return Task.FromResult(new ServiceResponse { Success = false });
            }

            return Task.FromResult(new ServiceResponse { Success = true });
        }

        public virtual Task<ServiceResponse> ValidateUpdateAsync(int id, TEntity entity)
        {
            // tên của các property cần check trùng
            var properties = entity.GetType().GetCustomAttributes(typeof(ValidateDuplicateAttribute), true);
            if (properties.Length == 0) return Task.FromResult(new ServiceResponse { Success = true });

            // tạo một mảng query để kiểm tra trùng
            IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable();
            foreach (var property in properties)
            {
                var propertyInfo = entity.GetType().GetProperty(((ValidateDuplicateAttribute)property).GetName());
                var value = propertyInfo.GetValue(entity);

                // Lấy biểu thức tương ứng với điều kiện tìm kiếm
                var exp = ExpressionBuilder.GetExpression<TEntity>(new FilterDefinition { Operand = Operand.Equal, Field = propertyInfo.Name, Value = value?.ToString() });
                if (exp != null) query = query.Where(exp);
            }

            // lấy ra attribute key hiện tại để loại bỏ nó khỏi query
            var primaryKey = entity.GetType().GetProperties().FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute), true).Any());
            var keyExp = ExpressionBuilder.GetExpression<TEntity>(new FilterDefinition { Operand = Operand.NotEqual, Field = primaryKey.Name, Value = id.ToString() });
            if (keyExp != null) query = query.Where(keyExp);

            // kiểm tra xem có bản ghi nào trùng không
            if (query != null && query.Any())
            {
                return Task.FromResult(new ServiceResponse { Success = false });
            }

            return Task.FromResult(new ServiceResponse { Success = true });
        }

        public virtual Task<ServiceResponse> ValidateDeleteAsync(TEntity entity)
        {
            return Task.FromResult(new ServiceResponse { Success = true });
        }

        public virtual TDto MapToDto(TEntity entity)
        {
            return _mapper.Map<TDto>(entity);
        }
        public virtual void MapToEntity(TDto dto, TEntity entity)
        {
            _mapper.Map(dto, entity);
        }
    }
}
