using Aggregator.DTO;
using MediatR;

namespace Aggregator.Commands
{
    public class OrderAggregateCommandHandler : IRequestHandler<OrderAggregateCommand, List<OrderAggregate>>
    {
        private readonly OrderProto.OrderProtoService.OrderProtoServiceClient orderClient;

        public OrderAggregateCommandHandler(OrderProto.OrderProtoService.OrderProtoServiceClient orderClient)
        {
            this.orderClient = orderClient;
        }

        public Task<List<OrderAggregate>> Handle(OrderAggregateCommand request, CancellationToken cancellationToken)
        {
            var ordersGrpcResponse = orderClient.GetOrderByUserId(new OrderProto.OrderRequestGrpc { UserId = request.userId });
            var ordersList = MapToAggregatorOrders(ordersGrpcResponse);
            return Task.FromResult(ordersList);
        }

        private List<OrderAggregate> MapToAggregatorOrders(OrderProto.OrderByUserIdResponseGrpc ordersGrpc)
        {
            List<OrderAggregate> orders = new List<OrderAggregate>();
            foreach (var orderGrpc in ordersGrpc.Orders)
            {
                List<OrderItemAggregate> items = new List<OrderItemAggregate>();
                foreach (var item in orderGrpc.Items)
                {
                    items.Add(new OrderItemAggregate(item.ItemName, item.ItemPrice, item.Amount));
                }
                OrderAggregate order = new OrderAggregate(orderGrpc.OrderId, orderGrpc.OrderStatus, items);
                orders.Add(order);
            }
          return orders;
        }
    }
}
