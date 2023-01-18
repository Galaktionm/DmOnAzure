using Aggregator.DTO;
using MediatR;

namespace Aggregator.Commands
{
    public class GetCartCommand : IRequest<Cart>
    {
        public string cartId { get; set; }

        public GetCartCommand(string cartId)
        {
           this.cartId = cartId;
        }

    }
}
