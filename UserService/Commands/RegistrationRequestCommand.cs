using MediatR;

namespace UserService.Commands
{
    public class RegistrationRequestCommand : IRequest<bool>
    {
        public string username { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public RegistrationRequestCommand(string userName, string email, string password)
        {
            this.username = userName;
            this.email = email;
            this.password = password;
        }

    }

    public record RegistrationResult
    {
        public string message { get; set; }

        public bool succeeded { get; set; }

        public RegistrationResult(string message, bool succeeded)
        {
            this.message = message;
            this.succeeded = succeeded;
        }
    }
}
