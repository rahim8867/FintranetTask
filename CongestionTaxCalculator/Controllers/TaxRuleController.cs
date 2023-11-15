using CongestionTaxCalculator.Core.Domain;
using CongestionTaxCalculator.Models.Taxs;
using CongestionTaxCalculator.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CongestionTaxCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxRuleController : ControllerBase
    {
        private readonly ITaxRuleService _taxRuleService;
        public TaxRuleController(ITaxRuleService taxRuleService)
        {
            _taxRuleService = taxRuleService;
        }

        [HttpGet(Name = "TaxRule")]
        public async Task<IList<TaxRule>> GetTaxRules(int cityId = 1)
        {
            return await _taxRuleService.GetTaxRules(cityId);
        }
    }
}
