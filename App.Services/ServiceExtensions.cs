using App.Services.Stocks;
using App.Services.Stocks.Validators;
using App.Services.StokHareketleri;
using App.Services.StokHareketleri.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace App.Services.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IStokHareketService, StokHareketService>();
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<UpdateStockRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateStockRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateStokHareketRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateStokHareketRequestValidator>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
