using Azure.Messaging.ServiceBus;

namespace Aggregator.EventBus
{
    public class EventBusConnection
    {
        public ServiceBusSender sender { get; set; }

        public EventBusConnection()
        {
          ServiceBusClient  client = new ServiceBusClient("Endpoint=sb://dundermifflinservicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=TgwWBR+vO1bwGqvFbYK3Z7a22CZTiEZioi1WlZ/szb8=");
          this.sender = client.CreateSender("orderingtopic");
        }
    }
}
