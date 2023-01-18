using System.ComponentModel.DataAnnotations;
using MediatR;

namespace UserService.Commands
{
    public class LoginRequestCommand : IRequest<LoginResult>
    {
        [Required(ErrorMessage = "Email can not be empty.")]
        public string email { get; set; } = null!;
        [Required(ErrorMessage = "Password can not be empty.")]
        public string password { get; set; } = null!;

        public LoginRequestCommand(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
    }

    public record LoginResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public string? Token { get; set; }

        public string UserId { get; set; }

        public LoginResult(bool Success, string Message, string Token, string UserId)
        {
            this.Success = Success;
            this.Message = Message;
            this.Token = Token;
            this.UserId = UserId;
        }

    }
}
