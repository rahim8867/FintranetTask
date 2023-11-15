using CongestionTaxCalculator.Models.Taxs;
using CongestionTaxCalculator.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CongestionTaxCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxRateController : ControllerBase
    {
        private readonly ITaxRateService _taxRateService;
        public TaxRateController(ITaxRateService taxRateService)
        {
            _taxRateService = taxRateService;
        }

        [HttpGet(Name = "TaxRate")]
        public async Task<IList<TaxRateDto>> GetTaxRates(int cityId = 1)
        {
            return await _taxRateService.GetTaxRates(cityId);
        }
    }
}
