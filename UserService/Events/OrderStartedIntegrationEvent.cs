namespace UserService.Events
{
    public class OrderStartedIntegrationEvent
    {
        public long orderId { get; set; }

        public string userId { get; set; }
        public List<OrderEventItem> eventItems { get; set; }

        public double totalPrice { get; set; }

        public OrderStartedIntegrationEvent(long orderId, string userId, List<OrderEventItem> eventItems, double totalPrice)
        {
            this.orderId = orderId;
            this.userId = userId;
            this.eventItems = eventItems;
            this.totalPrice = totalPrice;
        }
        
    }


    public record OrderEventItem
    {
        public string itemId { get; set; }
        public int amount { get; set; }

        public OrderEventItem(string itemId, int amount)
        {
            this.itemId = itemId;
            this.amount = amount;
        }
    }
}
