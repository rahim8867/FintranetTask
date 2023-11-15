using CongestionTaxCalculator.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace CongestionTaxCalculator.Data.Seed
{
    public static class SeedDataExtentions
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle() { Id = 1, Model = "M1", Type = VehicleType.Car, VehicleNo = "4574" },
                new Vehicle() { Id = 2, Model = "M2", Type = VehicleType.Emergency, VehicleNo = "2488" },
                new Vehicle() { Id = 3, Model = "B1", Type = VehicleType.Military, VehicleNo = "74878" },
                new Vehicle() { Id = 4, Model = "B2", Type = VehicleType.Car, VehicleNo = "45454" }
                );


            modelBuilder.Entity<VehiclePassTime>().HasData(
                new VehiclePassTime() { Id = 1, VehicleId = 1, Time = DateTime.Parse("2013-01-14 21:00:00") },
                new VehiclePassTime() { Id = 2, VehicleId = 1, Time = DateTime.Parse("2013-01-15 21:00:00") },
                new VehiclePassTime() { Id = 3, VehicleId = 1, Time = DateTime.Parse("2013-02-07 06:23:27") },
                new VehiclePassTime() { Id = 4, VehicleId = 1, Time = DateTime.Parse("2013-02-07 15:27:00") },
                new VehiclePassTime() { Id = 5, VehicleId = 1, Time = DateTime.Parse("2013-02-08 06:27:00") },
                new VehiclePassTime() { Id = 6, VehicleId = 1, Time = DateTime.Parse("2013-02-08 06:20:27") },
                new VehiclePassTime() { Id = 7, VehicleId = 1, Time = DateTime.Parse("2013-02-08 14:35:00") },
                new VehiclePassTime() { Id = 8, VehicleId = 1, Time = DateTime.Parse("2013-02-08 15:29:00") },
                new VehiclePassTime() { Id = 9, VehicleId = 1, Time = DateTime.Parse("2013-02-08 15:47:00") },
                new VehiclePassTime() { Id = 10, VehicleId = 1, Time = DateTime.Parse("2013-02-08 16:01:00") },
                new VehiclePassTime() { Id = 11, VehicleId = 1, Time = DateTime.Parse("2013-02-08 16:48:00") },
                new VehiclePassTime() { Id = 12, VehicleId = 1, Time = DateTime.Parse("2013-02-08 17:49:00") },
                new VehiclePassTime() { Id = 13, VehicleId = 1, Time = DateTime.Parse("2013-02-08 18:29:00") },
                new VehiclePassTime() { Id = 14, VehicleId = 1, Time = DateTime.Parse("2013-02-08 18:35:00") },
                new VehiclePassTime() { Id = 15, VehicleId = 1, Time = DateTime.Parse("2013-03-26 14:25:00") },
                new VehiclePassTime() { Id = 16, VehicleId = 1, Time = DateTime.Parse("2013-03-28 14:07:27") },
                new VehiclePassTime() { Id = 17, VehicleId = 2, Time = DateTime.Parse("2013-01-05 08:00:00") },
                new VehiclePassTime() { Id = 18, VehicleId = 2, Time = DateTime.Parse("2013-01-05 08:20:00") },
                new VehiclePassTime() { Id = 19, VehicleId = 2, Time = DateTime.Parse("2013-01-05 08:59:27") },
                new VehiclePassTime() { Id = 20, VehicleId = 2, Time = DateTime.Parse("2013-01-06 10:27:00") },
                new VehiclePassTime() { Id = 21, VehicleId = 2, Time = DateTime.Parse("2013-02-07 06:27:00") },
                new VehiclePassTime() { Id = 22, VehicleId = 2, Time = DateTime.Parse("2013-02-07 06:20:27") }
                );

            var city = new City() { Id = 1, Name = "Gothenburg" };
            modelBuilder.Entity<City>().HasData(city);

            modelBuilder.Entity<TaxRate>().HasData(
                new TaxRate() { Id = 1, CityId = city.Id, FromTime = TimeSpan.Parse("06:00"), ToTime = TimeSpan.Parse("06:29"), Amount = 8 },
                new TaxRate() { Id = 2, CityId = city.Id, FromTime = TimeSpan.Parse("06:30"), ToTime = TimeSpan.Parse("06:59"), Amount = 13 },
                new TaxRate() { Id = 3, CityId = city.Id, FromTime = TimeSpan.Parse("07:00"), ToTime = TimeSpan.Parse("07:59"), Amount = 18 },
                new TaxRate() { Id = 4, CityId = city.Id, FromTime = TimeSpan.Parse("08:00"), ToTime = TimeSpan.Parse("08:29"), Amount = 13 },
                new TaxRate() { Id = 5, CityId = city.Id, FromTime = TimeSpan.Parse("08:30"), ToTime = TimeSpan.Parse("14:59"), Amount = 8 },
                new TaxRate() { Id = 6, CityId = city.Id, FromTime = TimeSpan.Parse("15:00"), ToTime = TimeSpan.Parse("15:29"), Amount = 13 },
                new TaxRate() { Id = 7, CityId = city.Id, FromTime = TimeSpan.Parse("15:30"), ToTime = TimeSpan.Parse("16:59"), Amount = 18 },
                new TaxRate() { Id = 8, CityId = city.Id, FromTime = TimeSpan.Parse("17:00"), ToTime = TimeSpan.Parse("17:59"), Amount = 13 },
                new TaxRate() { Id = 9, CityId = city.Id, FromTime = TimeSpan.Parse("18:00"), ToTime = TimeSpan.Parse("18:29"), Amount = 8 },
                new TaxRate() { Id = 10, CityId = city.Id, FromTime = TimeSpan.Parse("18:30"), ToTime = TimeSpan.Parse("23:59"), Amount = 0 }, //| 18:30–05:59 | SEK 0  |
                new TaxRate() { Id = 11, CityId = city.Id, FromTime = TimeSpan.Parse("00:01"), ToTime = TimeSpan.Parse("05:59"), Amount = 0 }  //| 18:30–05:59 | SEK 0  |

                );
            #region Generate Holidays
            var dates = new[] {"2013/01/01","2013/03/28","2013/03/29","2013/04/01","2013/04/30","2013/05/01","2013/05/08","2013/05/09",
                              "2013/06/05","2013/06/06","2013/06/21","2013/11/01","2013/12/24","2013/12/25","2013/12/26","2013/12/31"};
            var holidays = dates.Select(DateOnly.Parse).ToList();
            for (int i = 1; i <= DateTime.DaysInMonth(2013, 7); i++)
                holidays.Add(new DateOnly(2013, 07, i));
            #endregion

            modelBuilder.Entity<TaxRule>().HasData(
                new TaxRule()
                {
                    Id = 1,
                    CityId = city.Id,
                    Name = "Weekends Free Tax For All",
                    DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Saturday, DayOfWeek.Sunday },
                    VehicleTypes = new List<VehicleType> { VehicleType.All },
                    Ratio = 0,
                    IsActive = true
                },
                 new TaxRule()
                 {
                     Id = 2,
                     CityId = city.Id,
                     Name = "Tax Exempt vehicles",
                     DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Saturday, DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday },
                     VehicleTypes = new List<VehicleType> { VehicleType.Military, VehicleType.Emergency, VehicleType.Buss, VehicleType.Motorcycle, VehicleType.Diplomat, VehicleType.Foreign },
                     Ratio = 0,
                     IsActive = true
                 },
                 new TaxRule()
                 {
                     Id = 3,
                     CityId = city.Id,
                     Name = "Holidays Free Tax For All",
                     VehicleTypes = new List<VehicleType> { VehicleType.Military, VehicleType.Emergency, VehicleType.Buss, VehicleType.Motorcycle, VehicleType.Diplomat, VehicleType.Foreign },
                     Ratio = 0,
                     SpecialDates = holidays,
                     ExpireTime = new DateTime(2013, 12, 31),
                     IsActive = true,
                 }
                );
        }
    }
}
