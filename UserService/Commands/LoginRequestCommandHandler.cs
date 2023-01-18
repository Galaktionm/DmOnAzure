using System.IdentityModel.Tokens.Jwt;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserService.Entities;
using UserService.Services;

namespace UserService.Commands
{
    public class LoginRequestCommandHandler : IRequestHandler<LoginRequestCommand, LoginResult>
    {
        private readonly JWTService jwtService;
        private readonly UserManager<User> userManager;

        public LoginRequestCommandHandler(JWTService jwtService, UserManager<User> userManager)
        {
            this.jwtService = jwtService;
            this.userManager = userManager;
        }

        public async Task<LoginResult> Handle(LoginRequestCommand request, CancellationToken cancellationToken)
        {

            var user = await userManager.FindByEmailAsync(request.email);
            if (user == null || !await userManager.CheckPasswordAsync(user, request.password))
            {
                throw new InvalidOperationException("Email or password is incorrect");
            }

            var secToken = await jwtService.GetTokenAsync(user);
            var jwt = new JwtSecurityTokenHandler().WriteToken(secToken);
            return new LoginResult(true, "Login successful", jwt, user.Id);
        }
    
    }
}
