using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Services
{
    public class CompareDbRepository : ICompareRepository
    {
        private readonly DatabaseContext databaseContext;

        public CompareDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public void Add(string userId, Item item)
        {
            var addCompareItem = TryGetByUserItem(userId, item);
            if (addCompareItem == null)
            {
                databaseContext.CompareItems.Add(new CompareItem
                {
                    UserId = userId,
                    Item = item
                });
                databaseContext.SaveChanges();
            }
        }

        public void Remove(string userId, Item item)
        {
            var removeCompareItem = TryGetByUserItem(userId, item);
            databaseContext.Remove(removeCompareItem);
            databaseContext.SaveChanges();
        }

        public void Clear(string userId)
        {
            var userCompareItems = databaseContext.CompareItems
                .Where(x => x.UserId == userId).ToList();
            databaseContext.CompareItems.RemoveRange(userCompareItems);
            databaseContext.SaveChanges();
        }

        public List<Item> GetAll(string userId)
        {
            var items = databaseContext.CompareItems
                .Where(x => x.UserId == userId)
                .Include(x => x.Item)
                .Select(x => x.Item)
                .ToList();
            return items;
        }

        public CompareItem TryGetByUserItem(string userId, Item item)
        {
            return databaseContext.CompareItems
                .FirstOrDefault(x => x.UserId == userId && x.Item.Id == item.Id);
        }
    }
}