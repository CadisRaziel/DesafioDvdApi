using DesafioDvD.Infrastructure.Context;
using DesafioDvD.Query.Infrastructure.Settings;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace DesafioDvD.WebApi.Setup
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDependencyInjection();

            #region Configurandobanco de dados
            services.AddDbContext<MoviesRentalWriteContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlConnection"), opt =>
                {
                    //Habilitando o RetryOnFailure, melhora nossa resiliencia com o banco de dados
                    opt.EnableRetryOnFailure();
                });
            });
            #endregion

            #region Configurando o REDIS
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
            });
            #endregion

            #region Configurando o MassTransit (ajuda com o RabbitMQ para que nao precisamos criar configuracao na mao)
            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration["EventBusSettings:HostAddress"]);
                });
            });
            #endregion

            #region Usando classe do mongodb como servico para pegar propriedades do meu arquivo de configuracao e vai repassar isso para outros servicos
            services.Configure<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)));
            //Da forma que esta acima vai me gerar um IoptionsValue de MongoDbSettings, ou seja ele nao vai me devolver a instancia de MongoDbSettings
            //porem o meu IptionsValue ele tem uma propriedade value que devolve a instancia, porem queremos a classe com o servico, para isso criaremos o codigo abaixo
            //Abaixo eu pego a propriedade value que eu comentei acima
            //(Caso outros servicos usassem esse MongoDbSettings o ideal seria crialo no buildingBlocks
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
            #endregion

            #region Configurando o Api Version
            //Ele ira configurar essas versoes em nossos controllers
            //[ApiVersion("1")]
            //[Route("api/v{version:apiVersion}/[controller]")]
            services.AddApiVersioning();
            #endregion

            #region Configurando os Health Check
            //Checa a saude das aplicacoes terceiras que estamos usando na nossa aplicacao
            //E interessante saber que o MassTransit tambem tem um health check configurado
            services.AddHealthChecks()
                .AddRedis(configuration["CacheSettings:ConnectionString"], "Cache HealthCheck", HealthStatus.Degraded)
                .AddMongoDb(configuration["MongoDbSettings:ConnectionString"], "MoviesRentalDb HealthCheck", HealthStatus.Degraded)
                .AddSqlServer(configuration.GetConnectionString("SqlConnection"));
            #endregion

            services.AddExceptionHandler<GlobalExceptionHandler>();

            return services;
        }
    }
}
