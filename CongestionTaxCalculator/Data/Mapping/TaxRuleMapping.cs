using CongestionTaxCalculator.Core.Domain;
using CongestionTaxCalculator.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json.Serialization;

namespace CongestionTaxCalculator.Data.Mapping
{
    public class TaxRuleMapping : IEntityTypeConfiguration<TaxRule>
    {
        public void Configure(EntityTypeBuilder<TaxRule> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).IsRequired();
            builder.Property(t => t.Ratio).IsRequired();
            builder.HasOne(t => t.City).WithMany(t => t.TaxRules).HasForeignKey(t => t.CityId);
            //According to usecase can be converted to table
            builder.Property(t => t.VehicleTypes).HasJsonConversion();
            builder.Property(t => t.DayOfWeeks).HasJsonConversion();
            builder.Property(t => t.SpecialDates).HasJsonConversion();
            builder.Property(t => t.ExpireTime);
            builder.Property(t => t.IsActive).IsRequired();
        }
    }
}
