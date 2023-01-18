using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Models
{
    public class Order
    {
        [Key]
        public long id { get; set; }

        public string buyerId { get; set; }

        public string orderStatus { get; set; }

        public List<OrderItem> items { get; set; }

        public Order()
        {
            items = new List<OrderItem>();
            orderStatus = "Started";
        }

        public Order(string buyerId, List<OrderItem> items)
        {
            this.buyerId = buyerId;
            this.orderStatus = "Started";
            this.items = items;
        }

        public Order(string buyerId, string status, List<OrderItem> items)
        {
            this.buyerId = buyerId;
            this.orderStatus = status;
            this.items = items;
        }
    }
}
