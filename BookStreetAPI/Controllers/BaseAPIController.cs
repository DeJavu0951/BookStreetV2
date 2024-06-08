using Application.DTO;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStreetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<TEntity, TDto, TService> : ControllerBase
    where TEntity : class
    where TDto : class
    where TService : IBaseService<TEntity, TDto>
    {
        public readonly TService _service;

        public BaseController(TService service)
        {
            _service = service;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            var entities = await _service.GetAllAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        [HttpPost]
        public virtual async Task<ServiceResponse> Create([FromBody] TDto dto)
        {
            if (!ModelState.IsValid)
                return new ServiceResponse { Success = false };

            return await _service.AddAsync(dto);
        }

        [HttpPut("{id}")]
        public virtual async Task<ServiceResponse> Update(int id, [FromBody] TDto dto)
        {
            if (!ModelState.IsValid)
                return new ServiceResponse { Success = false };

            return await _service.UpdateAsync(id, dto);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ServiceResponse> Delete(int id)
        {
            return await _service.DeleteAsync(id);
        }
    }
}
