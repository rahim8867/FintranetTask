using CongestionTaxCalculator.Core.Common;
using CongestionTaxCalculator.Core.Domain;
using CongestionTaxCalculator.Data;

namespace CongestionTaxCalculator.Services
{
    public class TaxRuleService: ITaxRuleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<TaxRule> _taxRuleRepository;
        public TaxRuleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _taxRuleRepository = new Repository<TaxRule>(unitOfWork);
        }

        public async Task<IList<TaxRule>> GetTaxRules()
        {
            return await _taxRuleRepository.GetAllAsync(x => x.IsActive);
        }
        public async Task<IList<TaxRule>> GetTaxRules(int cityId)
        {
            return await _taxRuleRepository.GetAllAsync(x => x.IsActive && x.CityId == cityId );
        }
    }
}
