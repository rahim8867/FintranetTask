using CongestionTaxCalculator.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CongestionTaxCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CongestionTaxController : ControllerBase
    {
        private readonly ICongestionTaxService _congestionTaxService;
        public CongestionTaxController(ICongestionTaxService congestionTaxService)
        {
            _congestionTaxService = congestionTaxService;
        }

        /// <summary>
        /// Calculate tax for vehicle in default range from 2013/01/01 to 2013/12/29
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
        [SwaggerOperation(Description = "Calculate tax for vehicle in default range from 2013/01/01 to 2013/12/29")]
        [HttpGet(Name = "CongestionTaxCalculator")]
        public async Task<decimal> GetVehicleTax(int cityId = 1, int vehicleId = 1)
        {
            return await _congestionTaxService.GetVehicleTax(cityId , vehicleId , new DateTime(2013,1,1) , new DateTime(2013,12,29));
        }
    }
}
