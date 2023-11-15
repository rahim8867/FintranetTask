using CongestionTaxCalculator.Core.Common;
using CongestionTaxCalculator.Core.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CongestionTaxCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IRepository<City> _cityRepository;
        public CityController(IRepository<City> cityRepository)
        {
            _cityRepository = cityRepository;
        }
        [HttpGet(Name = "City")]
        public async Task<dynamic> GetCities()
        {
            return await _cityRepository.GetAll().Select(i => new { i.Id, i.Name }).ToListAsync();
        }
    }
}
