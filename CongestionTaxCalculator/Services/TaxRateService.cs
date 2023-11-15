using CongestionTaxCalculator.Core.Common;
using CongestionTaxCalculator.Core.Domain;
using CongestionTaxCalculator.Data;
using CongestionTaxCalculator.Models.Taxs;
using Microsoft.EntityFrameworkCore;

namespace CongestionTaxCalculator.Services
{
    public class TaxRateService: ITaxRateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<TaxRate> _taxRateRepository;
        public TaxRateService(IUnitOfWork uow) 
        { 
            _unitOfWork = uow;
            _taxRateRepository = new Repository<TaxRate>(uow);
        }

        public async Task<IList<TaxRateDto>> GetTaxRates(int cityId)
        {
            var result = await _taxRateRepository.GetAll()
                .Where(i=> i.CityId == cityId)
                .Select(i => new TaxRateDto()
            {
                    Id = i.Id ,
                    FromTime = i.FromTime ,
                    ToTime = i.ToTime ,
                    Amount = i.Amount
            }).ToListAsync();

            return result;
        }

    }
}
