using CongestionTaxCalculator.Models.Vehicles;

namespace CongestionTaxCalculator.Services;

public interface IVehiclePassTimeService
{
    Task AddVehiclePassTime(AddVehiclePassTimeInput input);
    Task<IList<VehiclePassTimeDto>> GetVehiclePassTimes(int vehicleId, DateTime fromDateTime, DateTime toDateTime);
}
