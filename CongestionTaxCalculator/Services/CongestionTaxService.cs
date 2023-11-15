using CongestionTaxCalculator.Core.Common;
using CongestionTaxCalculator.Core.Domain;
using CongestionTaxCalculator.Data;
using CongestionTaxCalculator.Models.Taxs;
using Microsoft.Extensions.Caching.Memory;
using System.Collections;

namespace CongestionTaxCalculator.Services;

public class CongestionTaxService : ICongestionTaxService
{
    private IRepository<Vehicle> _vehicleRepository;
    private readonly ITaxRateService _taxRateService;
    private readonly IVehiclePassTimeService _vehicleTrafficService;
    private readonly ITaxRuleService _taxRuleService;
    private readonly IUnitOfWork _uow;
    private IMemoryCache _memoryCache;
    public CongestionTaxService(IUnitOfWork unitOfWork, ITaxRateService taxRateService, IVehiclePassTimeService vehicleTrafficService, ITaxRuleService taxRuleService, IMemoryCache memoryCache)
    {
        _taxRateService = taxRateService;
        _vehicleTrafficService = vehicleTrafficService;
        _taxRuleService = taxRuleService;
        _memoryCache = memoryCache;
        _uow = unitOfWork;
        _vehicleRepository = new Repository<Vehicle>(unitOfWork);
    }

    //in real app vehicle pass times must have toll station info (and station have city info) , so no need to pass cityId
    public async Task<decimal> GetVehicleTax(int cityId, int vehicleId, DateTime fromTime, DateTime toTime)
    {
        var vehicle = _vehicleRepository.Get(vehicleId);
        var vehicleTraffics = await _vehicleTrafficService.GetVehiclePassTimes(vehicleId, fromTime, toTime);

        decimal totalTax = 0;
        var groupedTraffics = vehicleTraffics.GroupBy(t => t.Time.Date);
        foreach (var traffic in groupedTraffics)
        {
            var taxPerDay = await CalculateTaxPerDay(cityId, vehicle, traffic.Select(i => i.Time).ToArray());
            totalTax += taxPerDay;
        }

        return totalTax;
    }

    record PassTimeTax(TimeSpan time, decimal fee);
    private async Task<decimal> CalculateTaxPerDay(int cityId, Vehicle vehicle, DateTime[] vehiclePassTimes)
    {
        var taxRates = await GetCachedTaxRates(cityId);
        var taxRules = await GetCachedTaxRules(cityId);

        //TODO Performance problem, when the number of traffic in a day is high for a car and only the car type should be checked,
        //which can be solved by adding a filter or type.
        
        //we can define rule with three option VehicleType , DayOfWeek , SpecialDates and zero or extra ratio

        decimal totalTaxPerDay = 0;
        var passTimeTaxes = new List<PassTimeTax>();
        foreach (var time in vehiclePassTimes)
        {
            var matchedRules = taxRules.Where(i => i.IsMatch(vehicle, time)).ToArray();
            var ratio = matchedRules.Length != 0 ? matchedRules.Sum(i => i.Ratio) : 1;
            var taxAmount = taxRates.FirstOrDefault(i => i.IsMatch(time.TimeOfDay))?.Amount ?? 0;
            var passTimeTax = ratio * taxAmount;
            passTimeTaxes.Add(new PassTimeTax(time.TimeOfDay, passTimeTax));
        }
        //TODO Read from setting
        //Select max one tax for multiple passes in 60 minute
        var rangeValue = 60;

        for (int i = 0; i < passTimeTaxes.Count; i++)
        {
            var startInterval = passTimeTaxes[i];
            var maxFee = passTimeTaxes[i].fee;
            if (i + 1 >= passTimeTaxes.Count)
            {
                totalTaxPerDay += maxFee;
                break;
            }
            var endInterval = startInterval.time.Add(TimeSpan.FromMinutes(rangeValue));
            var lastMatchIndex = passTimeTaxes.Count(t => t.time <= endInterval) - 1;
            if (lastMatchIndex != i)
            {
                maxFee = passTimeTaxes.GetRange(i , (lastMatchIndex - i) + 1).Max(i => i.fee);
                i = lastMatchIndex;
            }
            totalTaxPerDay += maxFee;

        }

        //TODO Read from setting
        //Max Tax Per Day
        if (totalTaxPerDay > 60)
            totalTaxPerDay = 60;


        return totalTaxPerDay;
    }


    private async Task<IList<TaxRule>> GetCachedTaxRules(int cityId)
    {
        const string cacheKey = "TaxRules";
        if (!_memoryCache.TryGetValue(cacheKey, out IList<TaxRule> cachedData))
        {
            cachedData = await _taxRuleService.GetTaxRules(cityId);
            _memoryCache.Set(cacheKey, cachedData, TimeSpan.FromMinutes(10));
        }

        return cachedData;
    }
    private async Task<IList<TaxRateDto>> GetCachedTaxRates(int cityId)
    {
        const string cacheKey = "TaxRates";
        if (!_memoryCache.TryGetValue(cacheKey, out IList<TaxRateDto> cachedData))
        {
            cachedData = await _taxRateService.GetTaxRates(cityId);
            _memoryCache.Set(cacheKey, cachedData, TimeSpan.FromMinutes(10));
        }

        return cachedData;
    }
}
