using Management.Api.Extensions;

namespace Management.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUi(this IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddPolicy("Cors", policy =>
                {
                    policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            services.AddDateOnlyTimeOnlyStringConverters(); // date only swagger converter

            return services;
        }
    }
}
