using MediatR;
using Ordering.API.Services;
using Ordering.Infrastructure;

namespace Ordering.API.Commands
{
    public class OrderValidationResultCommandHandler : IRequestHandler<OrderValidationResultCommand, bool>
    {
        private readonly OrderRepository repo;

        public OrderValidationResultCommandHandler(OrderRepository repo)
        {
            this.repo = repo;
        }

        public async Task<bool> Handle(OrderValidationResultCommand request, CancellationToken cancellationToken)
        {
            OrderValidationResult result = request.result;
            try
            {
                var order = await repo.GetOrderAsync(result.orderId);
                if (result.result == true)
                {
                    switch (result.service)
                    {
                        case "User":
                            if (order.orderStatus.Equals("Started"))
                            {
                                order.orderStatus = "Payment validated";
                                await repo.UpdateOrderAsync(order);
                                break;
                            }
                            else if (order.orderStatus.Equals("Products validated"))
                            {
                                order.orderStatus = "Validated";
                                await repo.UpdateOrderAsync(order);
                                break;
                            }
                            else
                            {
                                break;
                            }
                        case "Product":
                            if (order.orderStatus.Equals("Started"))
                            {
                                order.orderStatus = "Products validated";
                                await repo.UpdateOrderAsync(order);
                                break;
                            }
                            else if (order.orderStatus.Equals("Payment validated"))
                            {
                                order.orderStatus = "Validated";
                                await repo.UpdateOrderAsync(order);
                                break;
                            }
                            else
                            {
                                break;
                            }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
        }
    }
}
