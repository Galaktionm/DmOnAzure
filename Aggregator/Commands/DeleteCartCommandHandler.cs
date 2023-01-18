using Aggregator.Services;
using MediatR;
using StackExchange.Redis;

namespace Aggregator.Commands
{
    public class DeleteCartCommandHandler : IRequestHandler<DeleteCartCommand, bool>
    {
        private readonly IDatabase redis;

        public DeleteCartCommandHandler(RedisConnectionService service)
        {
            redis = service.connection.GetDatabase();
        }
        public async Task<bool> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            var result = await redis.KeyDeleteAsync(request.cartId);
            return result;
        }
    }
}
