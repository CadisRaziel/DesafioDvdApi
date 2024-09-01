
using DesafioDvD.Application;
using DesafioDvD.Infrastructure;
using DesafioDvD.Query.Infrastructure;
using DesafioDvD.Query.Application;
using DesafioDvD.WebApi.Cache;
namespace DesafioDvD.WebApi.Setup
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddWriteApplication();
            services.AddWriteInfrastructure();
            services.AddReadInfrastructure();
            services.AddReadApplication();
            services.AddScoped<ICacheRepository, CacheRepository>();         
            return services;
        }
    }
}
