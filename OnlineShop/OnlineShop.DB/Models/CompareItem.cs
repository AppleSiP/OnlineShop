namespace OnlineShop.Db.Models
{
    public class CompareItem
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public Item Item { get; set; }
    }
}
