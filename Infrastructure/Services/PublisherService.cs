using Application.DTO;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Services
{
    public class PublisherService : BaseService<Publisher, PublisherDTO>, IPublisherService
    {
        public PublisherService(IBaseRepository<Publisher> repository, IMapper mapper, BookStreetContext context) : base(repository, mapper, context)
        {
        }
    }
}
