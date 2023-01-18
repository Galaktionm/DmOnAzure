using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Models;
using Ordering.Domain.Repositories;

namespace Ordering.Infrastructure
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderingContext context;

        public OrderRepository(OrderingContext context)
        {
            this.context =context;
        }

        public async Task<Order> SaveOrderAsync(Order order)
        {
            context.Add(order);
            var x=await context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            var orderToUpdate=context.Orders.Find(order.id);
            if (orderToUpdate != null)
            {
                context.Update(order);
                await context.SaveChangesAsync();
                return context.Orders.Find(order.id);
            } else
            {
                throw new InvalidOperationException($"The order with the id of {order.id} not found");
            }         
        }

        public async Task<Order> ChangeStatusAsync(long id, string status)
        {
            var orderToUpdate = context.Orders.Find(id);
            if (orderToUpdate != null)
            {
                orderToUpdate.orderStatus = status;
                context.Update(orderToUpdate);
                await context.SaveChangesAsync();
                return context.Orders.Find(id);
            } else
            {
                throw new InvalidOperationException($"The order with the id of {id} not found");
            }
        }

        public async Task<bool> DeleteOrderAsync(long id)
        {
            var orderToDelete=context.Orders.Find(id);
            if(orderToDelete != null)
            {
                context.Remove(orderToDelete);
                await context.SaveChangesAsync();
                return true;
            } else
            {
                return false;
            }
        }

        public async Task<Order> GetOrderAsync(long id)
        {
            var order = await context.Orders.FindAsync(id);
            if( order != null)
            {
                return order;
            } else
            {
                throw new InvalidOperationException($"The order with the id of {id} not found");
            }
        }

        public List<Order> GetOrderByUserId(string userId)
        {
            var orders = context.Orders.Include(x=>x.items).Where(order => order.buyerId == userId).ToList();
            if(orders != null)
            {
                return orders;
            } else
            {
                throw new InvalidOperationException($"The order with the buyer id of {userId} not found");
            }    
        }

    }
}
