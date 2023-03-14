namespace Management.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUi(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
