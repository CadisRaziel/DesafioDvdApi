using DesafioDvD.Application.Contracts;
using DesafioDvD.Infrastructure.Context;
using DesafioDvD.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioDvD.Infrastructure
{
    public static class InfrastructureServiceCollection
    {
        //Essa classe e como se fosse nosso `module.export()` do javascript
        //Vamos exporta nossos servicos

        public static IServiceCollection AddWriteInfrastructure(this IServiceCollection services) {

            // AddScoped -> toda vez que a aplicacao precisar de uma instancia o primeiro metodo que chamou uma instancia de algum deles abaixo,
            // ele vai retornar uma nova instancia, vai ser uma instancia para cada metodo, porem dentro desse metodo por um acaso
            // eu tiver outros objetos que necessite de uma instancia ou estao pedindo por injecao de dependencia esses mesmo objetos, sera a mesma instancia do metodo acima
            // o que chamou primeiro

            //<IDvdsWriteRepository, DvdsWriteRepository>
            //Toda vez que eu pedir esse cara `IDvdsWriteRepository`
            //a aplicacao vai me entregar uma instancia desse cara `DvdsWriteRepository`

            services.AddScoped<MoviesRentalWriteContext>();
            services.AddScoped<IDvdsWriteRepository, DvdsWriteRepository>();
            services.AddScoped<IDirectorsWriteRepository, DirectorsWriteRepository>();

            return services;
        }
    }
}
