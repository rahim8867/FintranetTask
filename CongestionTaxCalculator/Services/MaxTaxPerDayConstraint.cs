using CongestionTaxCalculator.Core.Domain;

namespace CongestionTaxCalculator.Services
{
    public class MaxTaxPerDayConstraint : TaxRuleConstraint
    {
        public override int CheckConstraint(Vehicle vehicle, DateTime[] trafficTimes)
        {
            throw new NotImplementedException();
        }
    }
}
