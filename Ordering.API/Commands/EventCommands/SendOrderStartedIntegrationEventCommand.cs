using MediatR;
using Ordering.API.Events;

namespace Ordering.API.Commands
{
    public class SendOrderStartedIntegrationEventCommand : IRequest<bool>
    {
        public OrderStartedIntegrationEvent @event { get; set; }

        public SendOrderStartedIntegrationEventCommand(OrderStartedIntegrationEvent @event)
        {
            this.@event = @event;
        }

    }
}
