using CongestionTaxCalculator.Core.Common;
using CongestionTaxCalculator.Core.Domain;
using CongestionTaxCalculator.Data;
using CongestionTaxCalculator.Models.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace CongestionTaxCalculator.Services;

public class VehiclePassTimeService : IVehiclePassTimeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<VehiclePassTime> _vehicleTrafficRepository;
    private readonly IRepository<Vehicle> _vehicleRepository;
    public VehiclePassTimeService(IUnitOfWork uow)
    {
        _unitOfWork = uow;
        _vehicleTrafficRepository = new Repository<VehiclePassTime>(uow);
        _vehicleRepository = new Repository<Vehicle>(uow);
    }

    public async Task AddVehiclePassTime(AddVehiclePassTimeInput input)
    {
        //TODO check validation
        _vehicleTrafficRepository.Add(
            new VehiclePassTime
            {
                VehicleId = input.VehicleId,
                Time = input.Time
            });
        //save
       await _unitOfWork.SaveChangesAsync();

    }

    public async Task<IList<VehiclePassTimeDto>> GetVehiclePassTimes(int vehicleId , DateTime fromDateTime , DateTime toDateTime)
    {
        var query = _vehicleTrafficRepository.GetAll()
            .Where(i => i.VehicleId == vehicleId && i.Time >= fromDateTime && i.Time <= toDateTime);
  
        var result = await query.OrderBy(i => i.Time).Select(x => new VehiclePassTimeDto
        {
            Id = x.Id,
            Time = x.Time,
        }).ToListAsync();

        return result;

    }

 
}
