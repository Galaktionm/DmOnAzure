using System.Text.Json;
using Azure.Messaging.ServiceBus;
using MediatR;
using Ordering.API.EventBus;

namespace Ordering.API.Commands
{
    public class SendOrderStartedIntegrationEventCommandHandler : IRequestHandler<SendOrderStartedIntegrationEventCommand, bool>
    {
        private readonly EventBusConnection eventBus;

        public SendOrderStartedIntegrationEventCommandHandler(EventBusConnection eventBus)
        {
            this.eventBus = eventBus;
        }
        public async Task<bool> Handle(SendOrderStartedIntegrationEventCommand request, CancellationToken cancellationToken)
        {
            var eventJson=JsonSerializer.Serialize(request.@event);
            ServiceBusMessage message = new ServiceBusMessage(eventJson);
            await eventBus.sender.SendMessageAsync(message);
            return true;
        }

    }
}
