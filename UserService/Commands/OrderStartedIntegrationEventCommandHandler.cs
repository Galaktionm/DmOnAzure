using MediatR;
using Microsoft.AspNetCore.Identity;
using UserService.Entities;
using UserService.Configurations;

namespace UserService.Commands
{
    public class OrderStartedIntegrationEventCommandHandler : IRequestHandler<OrderStartedIntegrationEventCommand, OrderUserValidationResult>
    {
        private readonly EventBusConnection connection;
        private readonly UserManager<User> userManager;

        public OrderStartedIntegrationEventCommandHandler(EventBusConnection connection, UserManager<User> userManager)
        {
            this.connection = connection;
            this.userManager = userManager;
        }


        public async Task<OrderUserValidationResult> Handle(OrderStartedIntegrationEventCommand request, CancellationToken cancellationToken)
        {
            var orderEvent = request.@event;
            var user = await userManager.FindByIdAsync(orderEvent.userId);
            if (user != null && user.Balance>=orderEvent.totalPrice)
            {
                user.Balance-=orderEvent.totalPrice;
                return new OrderUserValidationResult(orderEvent.orderId, true);
            }

            return new OrderUserValidationResult(orderEvent.orderId, false);

        }
    }
}
