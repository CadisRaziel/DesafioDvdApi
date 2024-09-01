
using DesafioDvD.Consumer.Consumers.Director;
using DesafioDvD.Consumer.Consumers.Dvds;
using DesafioDvD.Core.EventBus;
using DesafioDvD.Query.Application;
using DesafioDvD.Query.Infrastructure;
using DesafioDvD.Query.Infrastructure.Settings;
using MassTransit;
using Microsoft.Extensions.Options;

namespace DesafioDvD.Consumer.Setup
{
    public static class ConsumerConfig
    {
        public static IServiceCollection AddConsumerConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)));
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
            services.AddReadApplication();
            services.AddReadInfrastructure();

            //Adicionando os consumer ao servico do massTransit        
            services.AddMassTransit(config =>
            {
                config.AddConsumer<DirectorCreatedConsumer>();
                config.AddConsumer<DirectorUpdatedConsumer>();
                config.AddConsumer<DirectorDeletedConsumer>();
                config.AddConsumer<DvdCreatedConsumer>();
                config.AddConsumer<DvdUpdatedConsumer>();
                config.AddConsumer<DvdDeletedConsumer>();
                config.AddConsumer<DvdRentedConsumer>();
                config.AddConsumer<DvdReturnedConsumer>();


                //Linkar os nossos consumers aos nossos endpoints
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    //Connection string do rabbitMQ
                    cfg.Host(configuration["EventBusSettings:HostAddress"]);

                    cfg.ReceiveEndpoint(EventBusConstants.CREATE_DIRECTOR_QUEUE, c =>
                    {
                        //Configurando esse consumer para essa fila `EventBusConstants.CREATE_DIRECTOR_QUEUE`
                        c.ConfigureConsumer<DirectorCreatedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.UPDATE_DIRECTOR_QUEUE, c =>
                    {
                        c.ConfigureConsumer<DirectorUpdatedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.DELETE_DIRECTOR_QUEUE, c =>
                    {
                        c.ConfigureConsumer<DirectorDeletedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.CREATE_DVD_QUEUE, c =>
                    {
                        c.ConfigureConsumer<DvdCreatedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.UPDATE_DVD_QUEUE, c =>
                    {
                        c.ConfigureConsumer<DvdUpdatedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.DELETE_DVD_QUEUE, c =>
                    {
                        c.ConfigureConsumer<DvdDeletedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.RENT_DVD_QUEUE, c =>
                    {
                        c.ConfigureConsumer<DvdRentedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.RETURN_DVD_QUEUE, c =>
                    {
                        c.ConfigureConsumer<DvdReturnedConsumer>(ctx);
                    });
                });
            });
          
            return services;
        }
    }
}
