using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregator.DTO
{
    public class AccountAggregate
    {
        public string email { get; set; }

        public string username { get; set; }

        public double balance { get; set; }

        public List<OrderAggregate> orders { get; set; }

        public AccountAggregate(string email, string username, double balance, List<OrderAggregate> orders)
        {
            this.email = email;
            this.username = username;
            this.balance = balance;
            this.orders = orders;
        }
    }
}
