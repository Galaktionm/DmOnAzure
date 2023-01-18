using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregator.DTO
{
    public record CartItem
    {
        public string itemId { get; set; }

        public string name { get; set; }

        public int price { get; set; }

        public int amount { get; set; }


        public CartItem(string itemId, string name, int price, int amount)
        {
            this.itemId = itemId;
            this.name = name;
            this.price = price;
            this.amount = amount;
        }
    }

}
