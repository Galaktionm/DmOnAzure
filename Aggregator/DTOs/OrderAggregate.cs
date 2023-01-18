using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregator.DTO
{
    public record OrderAggregate
    {
        public long orderId { get; set; }

        public string status { get; set; }

        public List<OrderItemAggregate> orderItems { get; set; }

        public OrderAggregate(long orderId, string status, List<OrderItemAggregate> orderItems)
        {
            this.orderId = orderId;
            this.status = status;
            this.orderItems = orderItems;
        }

    }
}
