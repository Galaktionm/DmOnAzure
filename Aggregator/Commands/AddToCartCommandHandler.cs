using System.Text.Json;
using Aggregator.Services;
using Aggregator.DTO;
using MediatR;
using StackExchange.Redis;

namespace Aggregator.Commands
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, CartItem>
    {
        private IDatabase redis;

        public AddToCartCommandHandler(RedisConnectionService redis)
        {
            this.redis = redis.connection.GetDatabase();
        }


        public async Task<CartItem> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var cart =await redis.StringGetAsync(request.cartId);

             if (cart.IsNullOrEmpty)
             {
                    List<CartItem> cartItems = new List<CartItem>();
                    cartItems.Add(request.CartItem);
                    Cart newCart = new Cart(request.cartId, cartItems);
                    var result=await redis.StringSetAsync(request.cartId, JsonSerializer.Serialize<Cart>(newCart));
                if (result == true)
                {
                    return request.CartItem;
                } else
                {
                    throw new InvalidOperationException("There was an error processing your request");
                }         
                }
             else
              {
                    var deserializedCart = JsonSerializer.Deserialize<Cart>(cart);
                    deserializedCart.items.Add(request.CartItem);
                    var result=await redis.StringSetAsync(request.cartId, JsonSerializer.Serialize<Cart>(deserializedCart));
                if (result == true)
                {
                    return request.CartItem;
                }
                else
                {
                    throw new InvalidOperationException("There was an error processing your request");
                }
              }
            }      
    }
}
