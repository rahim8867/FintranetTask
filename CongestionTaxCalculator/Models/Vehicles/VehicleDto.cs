using CongestionTaxCalculator.Core.Domain;

namespace CongestionTaxCalculator.Models.Vehicles
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string VehicleNo { get; set; }
        public VehicleType Type { get; set; }

    }
}
