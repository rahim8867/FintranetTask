using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CongestionTaxCalculator.Core.Domain;

public class TollStation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? StationNo { get; set; }
    public string? Location { get; set; }
    [ForeignKey(nameof(City))]
    public int CityId { get; set; }
    public City City { get; set; }
}
