using MediatR;
using ProductService.Events;

namespace ProductService.Commands
{
    public class OrderStartedIntegrationEventCommand : IRequest<OrderProductValidationResult>
    {
        public OrderStartedIntegrationEvent @event { get; set; }

        public OrderStartedIntegrationEventCommand(OrderStartedIntegrationEvent @event)
        {
            this.@event = @event;
        }

    }

    public record OrderProductValidationResult
    {
        public string service { get; set; }
        public long orderId { get; set; }

        public bool result { get; set; }

        public OrderProductValidationResult(long orderId, bool result)
        {
            this.service = "Product";
            this.orderId = orderId;
            this.result = result;
        }
   }
}
