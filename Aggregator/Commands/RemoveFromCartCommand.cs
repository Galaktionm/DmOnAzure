using Aggregator.DTO;
using MediatR;

namespace Aggregator.Commands
{
    public class RemoveFromCartCommand : IRequest<bool>
    {
        public string cartId { get; set; }
        public CartItem CartItem { get; set; }

        public RemoveFromCartCommand(string cartId, CartItem CartItem)
        {
            this.cartId = cartId;
            this.CartItem = CartItem;
        }

    }
}
