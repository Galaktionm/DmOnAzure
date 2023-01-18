using System.Text.Json;
using Aggregator.Services;
using Aggregator.DTO;
using MediatR;
using StackExchange.Redis;

namespace Aggregator.Commands
{
    public class GetCartItemsCommandHandler : IRequestHandler<GetCartItemsCommand, List<CartItem>>
    {
        
        private readonly IDatabase redis;
        public GetCartItemsCommandHandler(RedisConnectionService service)
        {
            redis = service.connection.GetDatabase();
        }
        public async Task<List<CartItem>> Handle(GetCartItemsCommand request, CancellationToken cancellationToken)
        {
            var cartJson =await redis.StringGetAsync(request.cartId);
            if (!cartJson.IsNullOrEmpty)
            {
                var cart=JsonSerializer.Deserialize<Cart>(cartJson);
                return cart.items;
            }

            throw new InvalidOperationException($"Cart with the id of {request.cartId} does not exist");

        }
    }
}
