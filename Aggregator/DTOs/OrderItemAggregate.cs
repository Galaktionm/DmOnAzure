using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregator.DTO
{
    public class OrderItemAggregate
    {
        public string name { get; set; }

        public double price { get; set; }

        public int amount { get; set; }

        public OrderItemAggregate(string name, double price, int amount)
        {
            this.name = name;
            this.price = price;
            this.amount = amount;
        }

    }
}
