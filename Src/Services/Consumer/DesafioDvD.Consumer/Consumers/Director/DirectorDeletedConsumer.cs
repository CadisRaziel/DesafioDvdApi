using DesafioDvD.Core.EventBus.Events;
using DesafioDvD.Query.Application.Features.Directors.Commands.DeleteDirector;
using MassTransit;
using MediatR;

namespace DesafioDvD.Consumer.Consumers.Director
{
    public class DirectorDeletedConsumer : IConsumer<DirectorDeletedEvent>
    {
        private readonly IMediator _mediator;
        private ILogger<DirectorDeletedConsumer> _logger;

        public DirectorDeletedConsumer(IMediator mediator, ILogger<DirectorDeletedConsumer> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<DirectorDeletedEvent> context)
        {
            try
            {
                var @event = context?.Message ?? throw new ArgumentNullException(nameof(context), "Invalid message");
                var command = new DeleteDirectorCommand(@event.Id);

                _logger.LogInformation($"Removing director {@event.Id}");
                var response = await _mediator.Send(command, default);

                if (!response)
                    throw new InvalidOperationException($"Something wrong happened during the process of removing director {@event.Id}");

                _logger.LogInformation($"Director {@event.Id} removed successfully");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
