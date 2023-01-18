using MediatR;
using UserService.Events;

namespace UserService.Commands
{
    public class OrderStartedIntegrationEventCommand : IRequest<OrderUserValidationResult>
    {
        public OrderStartedIntegrationEvent @event { get; set; }

        public OrderStartedIntegrationEventCommand(OrderStartedIntegrationEvent @event)
        {
            this.@event = @event;
        }

    }

    public record OrderUserValidationResult
    {
        public string service { get; set; }
        public long orderId { get; set; }

        public bool result { get; set; }

        public OrderUserValidationResult(long orderId, bool result)
        {
            this.service = "User";
            this.orderId = orderId;
            this.result = result;
        }
    }
}
