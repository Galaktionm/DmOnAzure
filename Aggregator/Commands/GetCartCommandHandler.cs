using System.Text.Json;
using Aggregator.Services;
using Aggregator.DTO;
using MediatR;
using StackExchange.Redis;

namespace Aggregator.Commands
{
    public class GetCartCommandHandler : IRequestHandler<GetCartCommand, Cart>
    {
        private IDatabase redis;

        public GetCartCommandHandler(RedisConnectionService redis)
        {
            this.redis = redis.connection.GetDatabase();
        }
        public async Task<Cart> Handle(GetCartCommand request, CancellationToken cancellationToken)
        {
            var cartValue=await redis.StringGetAsync(request.cartId);

            if (!cartValue.IsNullOrEmpty)
            {
                var cart = JsonSerializer.Deserialize<Cart>(cartValue);
                return cart;
            }

            throw new InvalidOperationException($"Basket with the id of {request.cartId} not found");


        }
    }

}
