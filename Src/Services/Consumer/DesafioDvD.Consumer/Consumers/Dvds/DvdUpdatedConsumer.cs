using DesafioDvD.Core.EventBus.Events;
using DesafioDvD.Query.Application.Features.Dvds.Commands.UpdateDvd;
using MassTransit;
using MediatR;

namespace DesafioDvD.Consumer.Consumers.Dvds
{
    public class DvdUpdatedConsumer : IConsumer<DvdUpdatedEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DvdUpdatedConsumer> _logger;

        public DvdUpdatedConsumer(IMediator mediator, ILogger<DvdUpdatedConsumer> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<DvdUpdatedEvent> context)
        {
            try
            {
                var @event = context?.Message ?? throw new ArgumentNullException(nameof(context), "Invalid Message");

                var command = new UpdateDvdCommand(@event.Id, @event.Title, @event.Genre, @event.Published, @event.Copies, @event.DirectorId, @event.UpdatedAt);

                _logger.LogInformation($"Updating dvd {@event.Title}");
                var response = await _mediator.Send(command, default);

                if (!response)
                {
                    _logger.LogError($"Something wrong happened during the update of dvd {@event.Id}");
                    throw new InvalidOperationException($"Failed to update Dvd {@event.Id}");
                }

                _logger.LogInformation($"Dvd {@event.Id} successfully updated");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while consuming the DvdUpdatedEvent");
                throw;
            }
        }
    }
}
