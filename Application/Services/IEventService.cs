using Application.DTO;
using Domain.Entities;

namespace Application.Services
{
    public interface IEventService: IBaseService<Event, EventDTO>
    {
    }
}
