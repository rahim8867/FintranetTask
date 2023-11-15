using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CongestionTaxCalculator.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init_CongestionDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StationNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stations_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaxRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    FromTime = table.Column<TimeSpan>(type: "Time(0)", nullable: false),
                    ToTime = table.Column<TimeSpan>(type: "Time(0)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxRates_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaxRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ratio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DayOfWeeks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecialDates = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpireTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VehicleTypes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxRules_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehiclePassTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiclePassTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehiclePassTimes_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Gothenburg" });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "Model", "Type", "VehicleNo" },
                values: new object[,]
                {
                    { 1, "M1", 1, "4574" },
                    { 2, "M2", 6, "2488" },
                    { 3, "B1", 9, "74878" },
                    { 4, "B2", 1, "45454" }
                });

            migrationBuilder.InsertData(
                table: "TaxRates",
                columns: new[] { "Id", "Amount", "CityId", "FromTime", "ToTime" },
                values: new object[,]
                {
                    { 1, 8m, 1, new TimeSpan(0, 6, 0, 0, 0), new TimeSpan(0, 6, 29, 0, 0) },
                    { 2, 13m, 1, new TimeSpan(0, 6, 30, 0, 0), new TimeSpan(0, 6, 59, 0, 0) },
                    { 3, 18m, 1, new TimeSpan(0, 7, 0, 0, 0), new TimeSpan(0, 7, 59, 0, 0) },
                    { 4, 13m, 1, new TimeSpan(0, 8, 0, 0, 0), new TimeSpan(0, 8, 29, 0, 0) },
                    { 5, 8m, 1, new TimeSpan(0, 8, 30, 0, 0), new TimeSpan(0, 14, 59, 0, 0) },
                    { 6, 13m, 1, new TimeSpan(0, 15, 0, 0, 0), new TimeSpan(0, 15, 29, 0, 0) },
                    { 7, 18m, 1, new TimeSpan(0, 15, 30, 0, 0), new TimeSpan(0, 16, 59, 0, 0) },
                    { 8, 13m, 1, new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 17, 59, 0, 0) },
                    { 9, 8m, 1, new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 18, 29, 0, 0) },
                    { 10, 0m, 1, new TimeSpan(0, 18, 30, 0, 0), new TimeSpan(0, 23, 59, 0, 0) },
                    { 11, 0m, 1, new TimeSpan(0, 0, 1, 0, 0), new TimeSpan(0, 5, 59, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "TaxRules",
                columns: new[] { "Id", "CityId", "DayOfWeeks", "ExpireTime", "IsActive", "Name", "Ratio", "SpecialDates", "VehicleTypes" },
                values: new object[,]
                {
                    { 1, 1, "[\r\n  6,\r\n  0\r\n]", null, true, "Weekends Free Tax For All", 0m, "[]", "[\r\n  0\r\n]" },
                    { 2, 1, "[\r\n  6,\r\n  0,\r\n  1,\r\n  2,\r\n  3,\r\n  4,\r\n  5\r\n]", null, true, "Tax Exempt vehicles", 0m, "[]", "[\r\n  9,\r\n  6,\r\n  3,\r\n  4,\r\n  7,\r\n  8\r\n]" },
                    { 3, 1, "[]", new DateTime(2013, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Holidays Free Tax For All", 0m, "[\r\n  \"2013-01-01\",\r\n  \"2013-03-28\",\r\n  \"2013-03-29\",\r\n  \"2013-04-01\",\r\n  \"2013-04-30\",\r\n  \"2013-05-01\",\r\n  \"2013-05-08\",\r\n  \"2013-05-09\",\r\n  \"2013-06-05\",\r\n  \"2013-06-06\",\r\n  \"2013-06-21\",\r\n  \"2013-11-01\",\r\n  \"2013-12-24\",\r\n  \"2013-12-25\",\r\n  \"2013-12-26\",\r\n  \"2013-12-31\",\r\n  \"2013-07-01\",\r\n  \"2013-07-02\",\r\n  \"2013-07-03\",\r\n  \"2013-07-04\",\r\n  \"2013-07-05\",\r\n  \"2013-07-06\",\r\n  \"2013-07-07\",\r\n  \"2013-07-08\",\r\n  \"2013-07-09\",\r\n  \"2013-07-10\",\r\n  \"2013-07-11\",\r\n  \"2013-07-12\",\r\n  \"2013-07-13\",\r\n  \"2013-07-14\",\r\n  \"2013-07-15\",\r\n  \"2013-07-16\",\r\n  \"2013-07-17\",\r\n  \"2013-07-18\",\r\n  \"2013-07-19\",\r\n  \"2013-07-20\",\r\n  \"2013-07-21\",\r\n  \"2013-07-22\",\r\n  \"2013-07-23\",\r\n  \"2013-07-24\",\r\n  \"2013-07-25\",\r\n  \"2013-07-26\",\r\n  \"2013-07-27\",\r\n  \"2013-07-28\",\r\n  \"2013-07-29\",\r\n  \"2013-07-30\",\r\n  \"2013-07-31\"\r\n]", "[\r\n  9,\r\n  6,\r\n  3,\r\n  4,\r\n  7,\r\n  8\r\n]" }
                });

            migrationBuilder.InsertData(
                table: "VehiclePassTimes",
                columns: new[] { "Id", "Time", "VehicleId" },
                values: new object[,]
                {
                    { 1, new DateTime(2013, 1, 14, 21, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new DateTime(2013, 1, 15, 21, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, new DateTime(2013, 2, 7, 6, 23, 27, 0, DateTimeKind.Unspecified), 1 },
                    { 4, new DateTime(2013, 2, 7, 15, 27, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 5, new DateTime(2013, 2, 8, 6, 27, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 6, new DateTime(2013, 2, 8, 6, 20, 27, 0, DateTimeKind.Unspecified), 1 },
                    { 7, new DateTime(2013, 2, 8, 14, 35, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 8, new DateTime(2013, 2, 8, 15, 29, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 9, new DateTime(2013, 2, 8, 15, 47, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 10, new DateTime(2013, 2, 8, 16, 1, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 11, new DateTime(2013, 2, 8, 16, 48, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 12, new DateTime(2013, 2, 8, 17, 49, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 13, new DateTime(2013, 2, 8, 18, 29, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 14, new DateTime(2013, 2, 8, 18, 35, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 15, new DateTime(2013, 3, 26, 14, 25, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 16, new DateTime(2013, 3, 28, 14, 7, 27, 0, DateTimeKind.Unspecified), 1 },
                    { 17, new DateTime(2013, 1, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 18, new DateTime(2013, 1, 5, 8, 20, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 19, new DateTime(2013, 1, 5, 8, 59, 27, 0, DateTimeKind.Unspecified), 2 },
                    { 20, new DateTime(2013, 1, 6, 10, 27, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 21, new DateTime(2013, 2, 7, 6, 27, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 22, new DateTime(2013, 2, 7, 6, 20, 27, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stations_CityId",
                table: "Stations",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxRates_CityId",
                table: "TaxRates",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxRules_CityId",
                table: "TaxRules",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_VehiclePassTimes_VehicleId",
                table: "VehiclePassTimes",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "TaxRates");

            migrationBuilder.DropTable(
                name: "TaxRules");

            migrationBuilder.DropTable(
                name: "VehiclePassTimes");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
