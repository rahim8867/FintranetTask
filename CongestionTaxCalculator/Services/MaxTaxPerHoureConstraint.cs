using CongestionTaxCalculator.Core.Domain;

namespace CongestionTaxCalculator.Services
{
    public class MaxTaxPerHoureConstraint : TaxRuleConstraint
    {
        public override int CheckConstraint(Vehicle vehicle, DateTime[] trafficTimes)
        {
            throw new NotImplementedException();
        }
    }
}
