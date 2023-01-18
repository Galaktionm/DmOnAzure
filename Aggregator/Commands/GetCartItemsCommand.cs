using Aggregator.DTO;
using MediatR;

namespace Aggregator.Commands
{
    public class GetCartItemsCommand : IRequest<List<CartItem>>
    {
        public string cartId { get; set; }

        public GetCartItemsCommand(string cartId)
        {
            this.cartId = cartId;
        }
    }
}
