using MediatR;
using Ordering.API.Services;

namespace Ordering.API.Commands
{
    public class OrderValidationResultCommand : IRequest<bool>
    {
        public OrderValidationResult result { get; set; }

        public OrderValidationResultCommand(OrderValidationResult result)
        {
            this.result = result;
        }
    }
}
