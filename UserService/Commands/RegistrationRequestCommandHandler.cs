using MediatR;
using Microsoft.AspNetCore.Identity;
using UserService.Entities;

namespace UserService.Commands
{
    public class RegistrationRequestCommandHandler : IRequestHandler<RegistrationRequestCommand, bool>
    {
        private readonly UserManager<User> userManager;

        public RegistrationRequestCommandHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<bool> Handle(RegistrationRequestCommand request, CancellationToken cancellationToken)
        {
            if (await userManager.FindByEmailAsync(request.email) == null)
            {
                var user = new User(request.username, request.email, 5000);
                var result = await userManager.CreateAsync(user, request.password);

                if (result.Succeeded)
                {
                    var addRole = await userManager.AddToRoleAsync(user, "User");
                    if (addRole.Succeeded)
                    {
                        return true;
                    }
                    else
                    {
                        throw new InvalidOperationException("There was an error processing your request");
                    }
                }
                else
                {
                    throw new InvalidOperationException("There was an error processing your request");
                }
            }

            throw new InvalidOperationException($"User with the email {request.email} already exists");
        }
    }
}
