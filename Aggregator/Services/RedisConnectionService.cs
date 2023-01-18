using StackExchange.Redis;

namespace Aggregator.Services
{
    public class RedisConnectionService
    {
        public ConnectionMultiplexer connection;
        public RedisConnectionService()
        {
            connection = ConnectionMultiplexer.Connect("azureredisfr.redis.cache.windows.net:6380,password=TnSKMmL2WeUU5VR7XRN0jvXPmXFx6e9egAzCaMVLp48=,ssl=True,abortConnect=False");
        }
    }
}
