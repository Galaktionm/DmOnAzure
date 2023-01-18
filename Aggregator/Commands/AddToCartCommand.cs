using Aggregator.DTO;
using MediatR;

namespace Aggregator.Commands
{
    public class AddToCartCommand : IRequest<CartItem>
    {
        public string cartId { get; set; }
        public CartItem CartItem { get; set; }

        public AddToCartCommand(string cartId, CartItem CartItem)
        {
            this.cartId = cartId;
            this.CartItem = CartItem;
        }

    }
}
