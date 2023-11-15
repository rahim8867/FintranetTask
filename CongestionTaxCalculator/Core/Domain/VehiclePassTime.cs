using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CongestionTaxCalculator.Core.Domain;

public class VehiclePassTime
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [ForeignKey(nameof(Vehicle))]
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
    public DateTime Time { get; set; }

    //TODO We can add station info to vehicle traffic info
    //public int StationId { get; set; }
    //public Station Station { get; set; }
}
