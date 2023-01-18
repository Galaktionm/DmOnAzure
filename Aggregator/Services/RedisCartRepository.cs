using System.Text.Json;
using Aggregator.Commands;
using Aggregator.DTO;
using MediatR;
using StackExchange.Redis;

namespace Aggregator.Services
{
    public class RedisCartRepository
    {
        private readonly IMediator mediator;

        public RedisCartRepository(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Cart> GetCartAsync(string userId)
        {
            var command = new GetCartCommand(userId);
            try
            {
                var cart=await mediator.Send(command);
                return cart;
            } catch (InvalidOperationException ex)
            {
                throw ex;
            }
        }

        public async Task<CartItem> AddToCartAsync(string cartId, CartItem item)
        {
            var command = new AddToCartCommand(cartId, item);
            try
            {
                var cart = await mediator.Send(command);
                return cart;
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }

        }

        public async Task<bool> RemoveItemFromCartAsync(string cartId, CartItem request)
        {
            var command = new RemoveFromCartCommand(cartId, request);
            try
            {
                var cart = await mediator.Send(command);
                return cart;
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }

        }

        public async Task<bool> DeleteCartAsync(string id)
        {
            var command = new DeleteCartCommand(id);
            try
            {
                var cart = await mediator.Send(command);
                return cart;
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
        }


        public async Task<List<CartItem>> GetCartProductsAsync(string id)
        {
            var command = new GetCartItemsCommand(id);
            try
            {
                var cart = await mediator.Send(command);
                return cart;
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
        }
    }
}
