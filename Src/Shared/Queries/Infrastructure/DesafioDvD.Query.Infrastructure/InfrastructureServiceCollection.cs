using DesafioDvD.Query.Application.Contracts;
using DesafioDvD.Query.Infrastructure.Context;
using DesafioDvD.Query.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioDvD.Query.Infrastructure
{
    public static class InfrastructureServiceCollection
    {
        public static IServiceCollection AddReadInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IMoviesRentalReadContext, MoviesRentalReadContext>();
            services.AddScoped<IDirectorsQueryRepository, DirectorsQueryRepository>();
            services.AddScoped<IDvdsQueryRepository, DvdsQueryRepository>();

            return services;
        }
    }
}
