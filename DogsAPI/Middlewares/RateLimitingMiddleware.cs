using AspNetCoreRateLimit;

namespace DogsAPI.Middlewares
{
    internal static class RateLimitingMiddleware
    {
        internal static IServiceCollection AddRateLimiting(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();

            services.Configure<IpRateLimitOptions>(options => configuration.GetSection("IpRateLimitingSettings").Bind(options));

            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddInMemoryRateLimiting();

            return services;
        }

        internal static IApplicationBuilder UseRateLimiting(this IApplicationBuilder app)
        {
            app.UseIpRateLimiting();
            return app;
        }
    }
}
