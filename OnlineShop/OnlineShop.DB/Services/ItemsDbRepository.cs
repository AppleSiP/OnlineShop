using OnlineShop.Db.Models;

namespace OnlineShop.Db.Services
{
    public class ItemsDbRepository : IItemsRepository
    {
        private readonly DatabaseContext databaseContext;

        public ItemsDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public List<Item> GetAll()
        {
            return databaseContext.Items.ToList();
        }

        public Item TryGetById(Guid id)
        {
            return databaseContext.Items.FirstOrDefault(item => item.Id == id);
        }

        public void Add(Item item)
        {
            databaseContext.Items.Add(item);
            databaseContext.SaveChanges();
        }

        public void Edit(Item item)
        {
            var existingItem = databaseContext.Items.FirstOrDefault(x => x.Id == item.Id);
            if (existingItem != null)
            {
                existingItem.Name = item.Name;
                existingItem.Cost = item.Cost;
                existingItem.Material = item.Material;
                existingItem.Description = item.Description;
            }
            databaseContext.SaveChanges();
        }

        public void Remove(Item item)
        {
            databaseContext.Items.Remove(item);
            databaseContext.SaveChanges();
        }
    }
}
