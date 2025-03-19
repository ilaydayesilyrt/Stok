using App.Repositories.Interfaces;
using App.Repositories.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace App.Repositories
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                var connectionStrings =
                   configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
                options.UseSqlServer(connectionStrings!.SqlServer,
                    sqlServerOptionsAction =>
                    {
                        sqlServerOptionsAction.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName);
                    });
            });
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IStokHareketleriRepository, StokHareketleriRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}