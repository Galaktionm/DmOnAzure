using System.Text.Json;
using Aggregator.Services;
using Aggregator.DTO;
using MediatR;
using StackExchange.Redis;

namespace Aggregator.Commands
{
    public class RemoveFromCartCommandHandler : IRequestHandler<RemoveFromCartCommand, bool>
    {
        private IDatabase redis;

        public RemoveFromCartCommandHandler(RedisConnectionService redis)
        {
            this.redis = redis.connection.GetDatabase();
        }

        public async Task<bool> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
        {
            var customerBasket = await redis.StringGetAsync(request.cartId);
            if (customerBasket.IsNullOrEmpty)
            {
                return false;
            }
            else
            {
                var cart = JsonSerializer.Deserialize<Cart>(customerBasket);
                var productForRemoval = cart.items.Find(cartProduct => cartProduct.itemId.Equals(request.CartItem.itemId));
                cart.items.Remove(productForRemoval);
                return await redis.StringSetAsync(request.cartId, JsonSerializer.Serialize(cart));
            }
        }
    }
}
