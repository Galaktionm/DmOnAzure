namespace Aggregator.DTOs
{
    public class AccountPageUserServiceResponse
    {
        public string username { get; set; }

        public string email { get; set; }

        public double balance { get; set; }

        public AccountPageUserServiceResponse(string username, string email, double balance)
        {
            this.username = username;
            this.email = email;
            this.balance = balance;
        }
    }
}
