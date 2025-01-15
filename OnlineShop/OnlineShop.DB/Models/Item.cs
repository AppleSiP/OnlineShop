namespace OnlineShop.Db.Models
{
    public class Item
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Cost { get; set; }

        public string Material { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }
    }
}
