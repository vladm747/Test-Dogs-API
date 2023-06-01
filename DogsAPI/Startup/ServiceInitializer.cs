using DAL.Context;
using DAL.Infrastructure.DI.Abstract;
using DAL.Infrastructure.DI.Implementation;
using Microsoft.EntityFrameworkCore;


namespace DogsAPI.Startup
{
    public static partial class ServiceInitializer
    {
        public static  IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            RegisterCustomDependencies(services);
            RegisterSwagger(services);
            //RegisterHttpClientDependencies(services);
            return services;
        }

        public static void RegisterCustomDependencies(IServiceCollection services)
        {
            services.AddDbContext<DogContext>(options => 
                options.UseSqlServer(Configur));
            services.AddScoped<IDogRepository, DogRepository>();
        }

        private static void RegisterSwagger(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}
