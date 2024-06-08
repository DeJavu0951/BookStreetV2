using Application.DTO;
using Application.Services;
using Domain.Entities;

namespace BookStreetAPI.Controllers
{
    public class EventController : BaseController<Event, EventDTO, IEventService>
    {
        public EventController(IEventService service) : base(service)
        {
        }
    }
}
