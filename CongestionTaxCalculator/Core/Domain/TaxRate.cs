using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CongestionTaxCalculator.Core.Domain;

public class TaxRate
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [ForeignKey(nameof(City))]
    public int CityId { get; set; }
    public City City { get; set; }
    [Column(TypeName ="Time(0)")]
    public TimeSpan FromTime { get; set; }
    [Column(TypeName = "Time(0)")]
    public TimeSpan ToTime { get; set; }
    public decimal Amount { get; set; }
    //public int CurrencyId { get; set; }
    //public Currency Currency { get; set; }

}
