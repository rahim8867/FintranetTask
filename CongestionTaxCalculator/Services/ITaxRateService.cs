using CongestionTaxCalculator.Models.Taxs;

namespace CongestionTaxCalculator.Services
{
    public interface ITaxRateService
    {
        Task<IList<TaxRateDto>> GetTaxRates(int cityId);
    }
}
