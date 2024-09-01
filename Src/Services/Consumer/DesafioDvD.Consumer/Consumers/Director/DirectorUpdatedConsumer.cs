using DesafioDvD.Core.EventBus.Events;
using DesafioDvD.Query.Application.Features.Directors.Commands.UpdateDirector;
using MassTransit;
using MediatR;

namespace DesafioDvD.Consumer.Consumers.Director
{
    public class DirectorUpdatedConsumer : IConsumer<DirectorUpdatedEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DirectorUpdatedConsumer> _logger;

        public DirectorUpdatedConsumer(IMediator mediator, ILogger<DirectorUpdatedConsumer> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<DirectorUpdatedEvent> context)
        {
            try
            {
                var @event = context?.Message ?? throw new ArgumentNullException(nameof(context), "Invalid message");
                var command = new UpdateDirectorCommand(@event.Id, @event.FullName, @event.UpdatedAt);

                _logger.LogInformation($"Updating Director {@event.FullName}");
                var response = await _mediator.Send(command, default);

                if (!response)
                {
                    throw new Exception($"Something wrong happened during the update of director {@event.FullName}");
                }
                _logger.LogInformation($"Director {@event.FullName} updated successfully");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
