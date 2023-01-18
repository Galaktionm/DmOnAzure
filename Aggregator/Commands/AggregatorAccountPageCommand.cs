using Aggregator.DTOs;
using MediatR;
using UserProto;

namespace Aggregator.Commands
{
    public class AggregatorAccountPageCommand : IRequest<AccountPageUserServiceResponse>
    {
        public string userId { get; set; }

        public AggregatorAccountPageCommand(string userId)
        {
            this.userId = userId;
        }
    }
}
