using Aggregator.DTO;
using MediatR;

namespace Aggregator.Commands
{
    public class OrderAggregateCommand : IRequest<List<OrderAggregate>>
    {
        public string userId { get; set; }

        public OrderAggregateCommand(string userId)
        {
            this.userId = userId;
        }

    }
}
