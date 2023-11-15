using CongestionTaxCalculator.Core.Domain;

namespace CongestionTaxCalculator.Services
{
    public interface ITaxRuleService
    {
        Task<IList<TaxRule>> GetTaxRules();
        Task<IList<TaxRule>> GetTaxRules(int cityId);
    }
}
