namespace OnlineShopWebApp.Models
{
    public class CartViewModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<CartItemViewModel> Items { get; set; }
        public decimal Cost
        {
            get
            {
                return Items.Sum(item => item.Cost);
            }
        }
        public int Amount
        {
            get
            {
                return Items.Sum(item => item.Amount);
            }
        }
    }
}