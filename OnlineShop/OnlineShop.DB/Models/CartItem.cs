namespace OnlineShop.Db.Models
{
    public class CartItem
    {
        public Guid Id { get; set; }

        public Item Item { get; set; }

        public int Amount { get; set; }
    }
}
