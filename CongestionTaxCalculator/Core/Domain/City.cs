using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CongestionTaxCalculator.Core.Domain;

public class City
{
    public City()
    {
        TaxRates = new List<TaxRate>();
        TaxRules = new List<TaxRule>();
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Name { get; set; }
    public virtual ICollection<TaxRule> TaxRules { get; set; }
    public virtual ICollection<TaxRate> TaxRates { get; set; }
}
