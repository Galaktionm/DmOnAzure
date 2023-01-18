using MediatR;
using ProductService.MongoDbConfig;

namespace ProductService.Commands
{
    public class OrderStartedIntegrationEventCommandHandler : IRequestHandler<OrderStartedIntegrationEventCommand, OrderProductValidationResult>
    {
        private readonly MongoDbService mongoDb;

        public OrderStartedIntegrationEventCommandHandler(MongoDbService mongoDb)
        {
            this.mongoDb = mongoDb;
        }


        public async Task<OrderProductValidationResult> Handle(OrderStartedIntegrationEventCommand request, CancellationToken cancellationToken)
        {
                var eventItems = request.@event.eventItems;
                Dictionary<string, int> productNumbers = new Dictionary<string, int>();
            if (eventItems != null)
            {
                foreach (var item in eventItems)
                {
                    try
                    {
                        var itemFromDb = await mongoDb.GetAsync(item.itemId);
                        if (itemFromDb != null && itemFromDb.available >= item.amount)
                        {
                            productNumbers.Add(item.itemId, item.amount);
                        }
                        else
                        {
                            return new OrderProductValidationResult(request.@event.orderId, false);
                        }
                    }
                    catch (Exception ex)
                    {
                        return new OrderProductValidationResult(request.@event.orderId, false);
                    }

                }
            }
            else { return new OrderProductValidationResult(request.@event.orderId, false); }
                

                foreach (var entry in productNumbers)
                {
                    var productToUpdate=await mongoDb.GetAsync(entry.Key);
                    productToUpdate.available-=entry.Value;
                    await mongoDb.UpdateAsync(productToUpdate);
                }

                return new OrderProductValidationResult(request.@event.orderId, true);
            
            } 
    }
}
