namespace CongestionTaxCalculator.Services
{
    public interface ICongestionTaxService
    {
        Task<decimal> GetVehicleTax(int cityId, int vehicleId, DateTime fromTime, DateTime toTime);
    }
}
