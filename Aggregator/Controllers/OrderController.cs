using Aggregator.Services;
using Aggregator.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Aggregator.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderProto.OrderProtoService.OrderProtoServiceClient orderClient;
        private readonly UserProto.UserProtoService.UserProtoServiceClient userClient;
        private readonly RedisCartRepository redis;

        public OrderController(OrderProto.OrderProtoService.OrderProtoServiceClient orderClient,
            UserProto.UserProtoService.UserProtoServiceClient userClient, RedisCartRepository redis)
        {
            this.orderClient = orderClient;
            this.userClient = userClient;
            this.redis = redis;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetOrder(string userId)
        {
            var order = await this.orderClient.GetOrderByUserIdAsync(new OrderProto.OrderRequestGrpc
            {
                UserId = userId
            });
            return Ok(MapToOrderAggregate(order));
        }


        [HttpPost("{userId}")]
        public async Task<IActionResult> PlaceOrder(string userId)
        {
            var cart=await redis.GetCartAsync(userId);
            var result=orderClient.SaveOrder(MapToSaveOrderRequestGrpc(cart));
            await redis.DeleteCartAsync(userId);
            if (result.Result == true)
            {
                return Ok(new {message=result.Message, result=result.Result});
            }
            return BadRequest(new { message = result.Message, result = result.Result });

        }


        private OrderProto.SaveOrderRequestGrpc MapToSaveOrderRequestGrpc(Cart cart)
        {
            OrderProto.SaveOrderRequestGrpc saveOrderRequestGrpc = new OrderProto.SaveOrderRequestGrpc
            {
                UserId = cart.userId
            };
            foreach(CartItem cartItem in cart.items)
            {
                saveOrderRequestGrpc.Items.Add(new OrderProto.OrderItemGrpc
                {
                    ItemId = cartItem.itemId,
                    ItemName = cartItem.name,
                    ItemPrice = cartItem.price,
                    Amount = cartItem.amount,
                });
            }
            return saveOrderRequestGrpc;
        }

        private List<OrderAggregate> MapToOrderAggregate(OrderProto.OrderByUserIdResponseGrpc grpc)
        {
            List<OrderAggregate> result=new List<OrderAggregate>();
            foreach(var grpcOrder in grpc.Orders)
            {
                foreach(var grpcItem in grpcOrder.Items)
                {
                    List<OrderItemAggregate> items = new List<OrderItemAggregate>();
                    items.Add(new OrderItemAggregate(grpcItem.ItemName, grpcItem.ItemPrice, grpcItem.Amount));
                    OrderAggregate orderAggregate = new OrderAggregate(grpcOrder.OrderId, grpcOrder.OrderStatus, items);
                    result.Add(orderAggregate);
                }
            }
            return result;
        }


    }
}
