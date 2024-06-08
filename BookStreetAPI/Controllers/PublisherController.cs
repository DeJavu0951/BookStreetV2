using Application.DTO;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStreetAPI.Controllers
{
    public class PublisherController : BaseController<Publisher, PublisherDTO, IPublisherService> 
    {
        public PublisherController(IPublisherService service) : base(service)
        {
        }
    }
}
