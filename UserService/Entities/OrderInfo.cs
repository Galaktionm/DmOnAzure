using System.ComponentModel.DataAnnotations;

namespace UserService.Entities
{
    public class OrderInfo
    {
        [Key]
        public long orderId { get; set; }

        public string userId { get; set; }

        public OrderInfo(long orderId, string userId)
        {
            this.orderId = orderId;
            this.userId = userId;
        }


    }
}
