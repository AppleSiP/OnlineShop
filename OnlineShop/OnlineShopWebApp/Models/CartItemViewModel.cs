namespace OnlineShopWebApp.Models
{
    public class CartItemViewModel
    {
        public Guid Id { get; set; }
        public ItemViewModel Item { get; set; }
        public int Amount { get; set; }
        public decimal Cost
        {
            get
            {
                return Item.Cost * Amount;
            }
        }
    }
}