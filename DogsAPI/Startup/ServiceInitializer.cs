using BLL.AutoMapperProfile;
using BLL.Services.DI.Abstract;
using BLL.Services.DI.Implementation;
using DAL.Context;
using DAL.Infrastructure.DI.Abstract;
using DAL.Infrastructure.DI.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace DogsAPI.Startup
{
    public static class ServiceInitializer
    {
        public static  IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterCustomDependencies(services, configuration);
            RegisterSwagger(services);
            return services;
        }

        public static void RegisterCustomDependencies(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DogContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DogsAPI")));
            services.AddAutoMapper(typeof(DogProfile));
            services.AddScoped<IDogRepository, DogRepository>();
            services.AddScoped<IDogService, DogService>();
        }

        private static void RegisterSwagger(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}
