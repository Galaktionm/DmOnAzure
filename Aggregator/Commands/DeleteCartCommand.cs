using MediatR;

namespace Aggregator.Commands
{
    public class DeleteCartCommand : IRequest<bool>
    {
        public string cartId { get; set; }

        public DeleteCartCommand(string cartId)
        {
            this.cartId = cartId;
        }
    }
}
