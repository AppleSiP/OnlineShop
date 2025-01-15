namespace OnlineShop.Db.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public UserDeliveryInfo UserDeliveryInfo { get; set; }

        public OrderStatus Status { get; set; }

        public List<CartItem> Items{ get; set; }

        public DateTime CreatedDataTime { get; set; }

        public Order()
        {
            Status = OrderStatus.Confirmation;
            CreatedDataTime = DateTime.Now;
        }
    }
}