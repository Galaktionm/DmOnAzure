using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Models
{
    public class OrderItem
    {
        [Key]
        public string id { get; set; }

        public string name { get; set; }

        public double price { get; set; }

        public int amount { get; set; }

        public OrderItem() { }

        public OrderItem(string id, string name, double price, int amount)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.amount = amount;
        }


    }
}
