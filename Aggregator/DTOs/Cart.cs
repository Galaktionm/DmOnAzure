using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregator.DTO
{
    public class Cart
    {
        public string userId { get; set; }

        public List<CartItem> items { get; set; }

        public Cart(string userId, List<CartItem> items)
        {
            this.userId = userId;
            this.items = items;
        }

    }
}
