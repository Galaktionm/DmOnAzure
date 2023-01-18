using MediatR;
using UserProto;

namespace UserService.Commands
{
    public class AccountPageCommand : IRequest<AccountPageUserServiceResponseGrpc>
    {
        public AccountPageUserServiceRequestGrpc UserIdGrpc { get; set; }

        public AccountPageCommand(AccountPageUserServiceRequestGrpc UserIdGrpc)
        {
            this.UserIdGrpc = UserIdGrpc;
        }

    }

}
