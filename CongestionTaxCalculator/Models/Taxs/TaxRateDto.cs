using CongestionTaxCalculator.Core.Domain;

namespace CongestionTaxCalculator.Models.Taxs
{
    public class TaxRateDto
    {
        public int Id { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public decimal Amount { get; set; }
        public bool IsMatch(TimeSpan time)
        {
            return time >= FromTime && time <= ToTime;
        }
    }
}
