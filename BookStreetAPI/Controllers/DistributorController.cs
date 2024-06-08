using Application.DTO;
using Application.Services;
using Domain.Entities;

namespace BookStreetAPI.Controllers
{
    public class DistributorController : BaseController<Distributor, DistributorDTO, IDistributorService>
    {
        public DistributorController(IDistributorService service) : base(service)
        {
        }
    }
}
