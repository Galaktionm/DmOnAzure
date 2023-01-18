using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ordering.Domain.Models;

namespace Ordering.Domain.Repositories
{
    public interface IOrderRepository
    {
        public Task<Order> SaveOrderAsync(Order order);

        public Task<Order> UpdateOrderAsync(Order order);

        public Task<bool> DeleteOrderAsync(long id);

        public Task<Order> ChangeStatusAsync(long id, string status);

        public Task<Order> GetOrderAsync(long id);

        public List<Order> GetOrderByUserId(string userId);
    }
}
