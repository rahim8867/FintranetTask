using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CongestionTaxCalculator.Core.Domain;

public class TaxRule
{
    public TaxRule()
    {
        DayOfWeeks = new List<DayOfWeek>();
        VehicleTypes = new List<VehicleType>();
        SpecialDates = new List<DateOnly>();
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal Ratio { get; set; }
    public List<DayOfWeek> DayOfWeeks { get; set; }
    //when set date , dayOfWeeks must cleard
    public List<DateOnly> SpecialDates { get; set; }
    public DateTime? ExpireTime { get; set; }
    //better to consider VehicleType as table
    public List<VehicleType> VehicleTypes { get; set; }
    public int CityId { get; set; }
    public City City { get; set; }
    public bool IsActive { get; set; }

    public bool IsMatch(Vehicle vehicle , DateTime date)
    {
        if(ExpireTime.HasValue && date > ExpireTime.Value)
            return false;
        if (VehicleTypes.Count != 0 &&!VehicleTypes.Contains(vehicle.Type))
                return false;
        if(DayOfWeeks.Count != 0 && !DayOfWeeks.Contains(date.DayOfWeek))
                return false;
        if (SpecialDates.Count != 0 && !SpecialDates.Contains(DateOnly.FromDateTime(date)))
                return false;

        return true;
    }

}
