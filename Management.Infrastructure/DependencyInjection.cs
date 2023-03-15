using Management.Application.Common.Interfaces;
using Management.Application.Common.Interfaces.Repositories;
using Management.Application.Common.Interfaces.Services;
using Management.Infrastructure.Persistance;
using Management.Infrastructure.Repository;
using Management.Infrastructure.Services;
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

            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}
