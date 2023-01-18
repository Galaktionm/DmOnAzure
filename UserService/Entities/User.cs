using Microsoft.AspNetCore.Identity;

namespace UserService.Entities
{
    public class User : IdentityUser
    {
        public double Balance { get; set; }
        public List<OrderInfo> Orders { get; set; }
        public User()
        {
            Orders = new List<OrderInfo>();
        }
        public User(string userName, string email, double balance)
        {
            Orders = new List<OrderInfo>();
            UserName = userName;
            Email = email;
            this.Balance = balance;
        }
    }
}
