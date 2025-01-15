using OnlineShopWebApp.Areas.Admin.Models;

namespace OnlineShopWebApp.Models
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public UserDeliveryInfoViewModel UserDeliveryInfo { get; set; }
        public OrderStatusViewModel Status { get; set; }
        public List<CartItemViewModel> Items { get; set; }
        public DateTime CreatedDataTime { get; set; }

        public OrderViewModel()
        {
            Id = Guid.NewGuid();
            Status = OrderStatusViewModel.Confirmation;
            CreatedDataTime = DateTime.Now;
        }
    }
}