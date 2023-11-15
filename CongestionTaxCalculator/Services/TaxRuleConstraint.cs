using CongestionTaxCalculator.Core.Domain;

namespace CongestionTaxCalculator.Services
{
    public abstract class TaxRuleConstraint
    {
        public abstract int CheckConstraint(Vehicle vehicle, DateTime[] trafficTimes);
    }
}
