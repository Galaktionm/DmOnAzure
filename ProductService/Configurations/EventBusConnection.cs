using Azure.Messaging.ServiceBus;

namespace ProductService.Configurations
{
    public class EventBusConnection
    {
        public ServiceBusSender sender { get; set; }
        public ServiceBusProcessor processor { get; set; }

        public EventBusConnection()
        {
            ServiceBusClient client = new ServiceBusClient("Endpoint=sb://dundermifflinservicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=TgwWBR+vO1bwGqvFbYK3Z7a22CZTiEZioi1WlZ/szb8=");
            this.sender = client.CreateSender("orderservicequeue");
            this.processor = client.CreateProcessor("orderingtopic", "productservicesubscription");
        }
    }
}
