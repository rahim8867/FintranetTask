
using CongestionTaxCalculator.Core.Domain;
using CongestionTaxCalculator.Data.Mapping;
using CongestionTaxCalculator.Data.Seed;
using Microsoft.EntityFrameworkCore;

namespace CongestionTaxCalculator.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {

    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        
    }

    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<TollStation> Stations { get; set; }
    public DbSet<TaxRule> TaxRules { get; set; }
    public DbSet<TaxRate> TaxRates { get; set; }
    public DbSet<VehiclePassTime> VehiclePassTimes { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TaxRuleMapping());

        modelBuilder.SeedData();
    }
}
