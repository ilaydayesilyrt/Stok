using App.Repositories;
using App.Repositories.Interfaces;
using App.Services.Extensions;
using App.Services.Filters;
using App.Services.Stocks;
using App.Services.StokHareketleri;
using Microsoft.OpenApi.Models;
using System;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<FluentValidationFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IStokHareketService, StokHareketService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);
builder.Services.AddScoped<AppDbContext>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1"));
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
