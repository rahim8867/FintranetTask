using CongestionTaxCalculator.Core.Common;
using CongestionTaxCalculator.Core.Domain;
using CongestionTaxCalculator.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CongestionTaxCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IRepository<Vehicle> _vehicleRepository;
        public VehicleController(IRepository<Vehicle> vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }
        [HttpGet(Name ="Vehicle")]
        public async Task<dynamic> GetCities()
        {
            return await _vehicleRepository.GetAll().Select(i => new { i.Id ,i.Model , i.Type , i.VehicleNo}).ToListAsync();
        }
    }
}
