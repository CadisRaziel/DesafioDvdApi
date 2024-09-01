using DesafioDvD.Core.EventBus.Events;
using DesafioDvD.Query.Application.Features.Directors.Commands.CreateDirector;
using MassTransit;
using MediatR;


namespace DesafioDvD.Consumer.Consumers.Director
{
    //IConsumer -> MassTransit
    //Ele vai consumir o meu `DirectorCreatedEvent`
    public class DirectorCreatedConsumer : IConsumer<DirectorCreatedEvent>
    {
        private readonly IMediator _mediator;
        private ILogger<DirectorCreatedConsumer> _logger; //Identifica qual objeto deu problam e qual horario, tambem sabemos se houve uma exception sem tratamento

        public DirectorCreatedConsumer(IMediator mediator, ILogger<DirectorCreatedConsumer> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<DirectorCreatedEvent> context)
        {
            try
            {
                var @event = context?.Message ?? throw new ArgumentNullException(nameof(context));
                var command = new CreateDirectorCommand(@event.Id, @event.FullName, @event.CreatedAt, @event.UpdatedAt);
                _logger.LogInformation($"Creating director {command.FullName}");

                var response = await _mediator.Send(command, default);
                if (!response)
                {
                    _logger.LogError($"Something wrong happened during the creation of director {@event.Id}");
                    throw new InvalidOperationException($"Failed to create director {@event.Id}");
                }
                _logger.LogInformation($"Director {@event.Id}  created");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while consuming the DirectorCreatedEvent");
                throw;
            }
        }
    }
}
