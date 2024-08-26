using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DesafioDvD.Query.Application
{
    public static class ApplicationServiceCollection
    {
        public static IServiceCollection AddReadApplication(this IServiceCollection services)
        {
            //Vai pegar todos validators que eu criei e vai registrar no injetor de dependencia  
            //Esse AddValidatorsFromAssembly e do package FluentValidation.DependencyInjectionExtension            
            //No program.cs eu faria isso para cada 1 dos nossos validators (temos mais de 4) (porem nao estamos fazendo isso no program.cs por conta da abstracao que estamos fazendo)
            //No program.cs ficaria assim (porem aqui tem apenas 1 validator) \/
            /*     
            builder.Services.AddControllers(           
            options => options.Filters.Add(typeof(FiltersValidators))
            ).AddFluentValidation(v => v.RegisterValidatorsFromAssemblyContaining<CreateUserCommandValidator>());
             */
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Scoped);


            //Sempre que uma requisicao for lancada ele pega e manda pro handler correto
            //Tambem estamos injetando dependencia (porem nao estamos fazendo isso no program.cs por conta da abstracao que estamos fazendo)
            //no program.cs fariamos assim \/
            /*
             RegisterMediatR(builder.Services)
             */
            services.AddMediatR(options => options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


            return services;
        }
    } 
}

/*
 Classe criara para nos injetarmos os nossos servicoes 
 Instalamos uma dependencia aqui na `Desafio.Dvd.Application que se chama <FluentValidation.DependencyInjectionExtension> Para podermos realizar a injecao de dependencia
 */
