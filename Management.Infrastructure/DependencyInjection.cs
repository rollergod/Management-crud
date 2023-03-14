using Management.Application.Common.Interfaces;
using Management.Application.Common.Interfaces.Repositories;
using Management.Infrastructure.Persistance;
using Management.Infrastructure.Repository;
using Management.LoggerService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Management.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); // datetime postgres

            services.AddScoped<ILoggerManager, LoggerManager>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
