using MediatR;
using Microsoft.AspNetCore.Identity;
using UserProto;
using UserService.Entities;

namespace UserService.Commands
{
    public class AccountPageCommandHandler : IRequestHandler<AccountPageCommand, AccountPageUserServiceResponseGrpc>
    {
        private readonly UserManager<User> userManager;
        public AccountPageCommandHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<AccountPageUserServiceResponseGrpc> Handle(AccountPageCommand request, CancellationToken cancellationToken)
        {
            var user=await userManager.FindByIdAsync(request.UserIdGrpc.UserId);

            if (user != null)
            {
                var response=new AccountPageUserServiceResponseGrpc{
                    Username=user.UserName,
                    Email=user.Email,
                    Balance=user.Balance,
                };

                return response;
            }

            throw new ArgumentException($"User with the id of {request.UserIdGrpc.UserId} not found");
            
        }

 
    }
}
