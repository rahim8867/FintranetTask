using CongestionTaxCalculator.Core.Common;
using CongestionTaxCalculator.Data;
using CongestionTaxCalculator.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITaxRateService, TaxRateService>();
builder.Services.AddScoped<ITaxRuleService, TaxRuleService>();
builder.Services.AddScoped<IVehiclePassTimeService, VehiclePassTimeService>();
builder.Services.AddScoped<ICongestionTaxService, CongestionTaxService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var dbConnectionStr = builder.Configuration.GetConnectionString("SqlServer");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(dbConnectionStr));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});
builder.Services.AddMemoryCache();

var app = builder.Build();

app.Services.CreateScope();
using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
dbContext.Database.EnsureCreated();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
