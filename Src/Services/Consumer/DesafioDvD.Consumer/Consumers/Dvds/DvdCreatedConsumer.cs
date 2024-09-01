﻿using DesafioDvD.Core.EventBus.Events;
using DesafioDvD.Query.Application.Features.Dvds.Commands.CreateDvd;
using MassTransit;
using MediatR;

namespace DesafioDvD.Consumer.Consumers.Dvds
{
    public class DvdCreatedConsumer : IConsumer<DvdCreatedEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DvdCreatedConsumer> _logger;

        public DvdCreatedConsumer(IMediator mediator, ILogger<DvdCreatedConsumer> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<DvdCreatedEvent> context)
        {
            try
            {
                var @event = context?.Message ?? throw new ArgumentNullException(nameof(context), "Invalid message");
                var command = new CreateDvdCommand(
                    @event.Id,
                    @event.Title,
                    @event.Genre,
                    @event.Published,
                    @event.Available,
                    @event.Copies,
                    @event.DirectorId,
                    @event.CreatedAt,
                    @event.UpdatedAt);

                _logger.LogInformation($"Creating dvd {command.Title}");
                var response = await _mediator.Send(command, default);

                if (!response)
                    throw new InvalidOperationException($"Something wrong happened during the creation of dvd {command.Id}");

                _logger.LogInformation($"Dvd {command.Title} created successfully");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
