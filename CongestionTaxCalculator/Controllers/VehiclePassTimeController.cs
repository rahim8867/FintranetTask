using CongestionTaxCalculator.Models.Vehicles;
using CongestionTaxCalculator.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CongestionTaxCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclePassTimeController : ControllerBase
    {
        private readonly IVehiclePassTimeService _vehiclePassTimeService;
        public VehiclePassTimeController(IVehiclePassTimeService vehiclePassTimeService)
        {
            _vehiclePassTimeService = vehiclePassTimeService;
        }
        [HttpGet(Name = "VehiclePassTime")]
        public async Task<IList<VehiclePassTimeDto>> GetVehiclePassTimes(int vehicleId = 1)
        {
            return await _vehiclePassTimeService.GetVehiclePassTimes(vehicleId, new DateTime(2013, 1, 1), new DateTime(2013, 12, 29));
        }
    }
}
