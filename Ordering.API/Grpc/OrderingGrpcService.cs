using Grpc.Core;
using MediatR;
using Ordering.API.Commands;
using Ordering.API.Events;
using Ordering.Domain.Models;
using Ordering.Infrastructure;
using OrderProto;

namespace Ordering.API
{
    public class OrderingGrpcService : OrderProtoService.OrderProtoServiceBase
    {

        private readonly OrderRepository repository;
        private readonly IMediator mediator;

        public OrderingGrpcService(OrderRepository repository, IMediator mediator)
        {
            this.repository = repository;
            this.mediator= mediator;
        }

        public async override Task<OrderGrpc> GetOrder(OrderRequestGrpc request, ServerCallContext context)
        {
            var order =await repository.GetOrderAsync(request.OrderId);
            if (order != null)
            {
               var response=MapToOrderResponseGrpc(order);
                return response;
            }

            return new OrderGrpc { };
        }

        public override Task<OrderByUserIdResponseGrpc> GetOrderByUserId(OrderRequestGrpc request, ServerCallContext context)
        {
            List<Order> orders=repository.GetOrderByUserId(request.UserId);
            var response = new OrderByUserIdResponseGrpc();
            foreach (var order in orders)
            {
                response.Orders.Add(MapToOrderResponseGrpc(order));
            }
            return Task.FromResult(response);
        }

        public async override Task<ResultResponseGrpc> SaveOrder(SaveOrderRequestGrpc order, ServerCallContext context)
        {
            var orderEntity=MapToOrderEntity(order);
            try
            {
                var savedOrder=await repository.SaveOrderAsync(orderEntity);
                var orderStartedEvent=CreateEvent(savedOrder);
                var orderCommand = new SendOrderStartedIntegrationEventCommand(orderStartedEvent);
                await this.mediator.Send(orderCommand);
                return new ResultResponseGrpc
                {
                    Message = $"Order saved. Validation might take time",
                    Result = true
                };
           } catch (Exception ex)
            {
                return new ResultResponseGrpc { 
                    Message=$"Could not save order",
                    Result = false             
                };
            }
        }


        private OrderGrpc MapToOrderResponseGrpc(Order order)
        {

            OrderGrpc response = new OrderGrpc
            {
                OrderId = order.id,
                UserId = order.buyerId,
                OrderStatus = order.orderStatus
            };
            foreach(var item in order.items)
            {
                response.Items.Add(new OrderItemGrpc
                {
                    ItemId = item.id,
                    ItemName = item.name,
                    ItemPrice = item.price,
                    Amount = item.amount
                });
            }
            return response;
        }

        private Order MapToOrderEntity(SaveOrderRequestGrpc orderGrpc)
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach(OrderItemGrpc grpcItem in orderGrpc.Items)
            {
                orderItems.Add(new OrderItem(grpcItem.ItemId, grpcItem.ItemName, grpcItem.ItemPrice, grpcItem.Amount));
            }
            Order order = new Order(orderGrpc.UserId, orderItems);
            return order;
        }

        private OrderStartedIntegrationEvent CreateEvent(Order order)
        {
            List<OrderEventItem> orderItems = new List<OrderEventItem>();
            double total = 0;
            foreach(var item in order.items)
            {
                orderItems.Add(new OrderEventItem(item.id, item.amount));
                total += item.price*item.amount;
            }
            OrderStartedIntegrationEvent @event = new OrderStartedIntegrationEvent(order.id, order.buyerId, orderItems, total);
            return @event;
        }

    }
}
