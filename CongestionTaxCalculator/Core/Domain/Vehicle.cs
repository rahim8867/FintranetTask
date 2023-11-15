using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CongestionTaxCalculator.Core.Domain;

public class Vehicle
{
    public Vehicle()
    {
        PassTimes = new List<VehiclePassTime>();
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Model { get; set; }
    public required string VehicleNo { get; set; }
    public VehicleType Type { get; set; }
    public virtual ICollection<VehiclePassTime> PassTimes { get; set; }
}
