using Application.DTO;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Services
{
    public class EventService : BaseService<Event, EventDTO>, IEventService
    {
        public EventService(IBaseRepository<Event> repository, IMapper mapper, BookStreetContext context) : base(repository, mapper, context)
        {
        }
    }
}
