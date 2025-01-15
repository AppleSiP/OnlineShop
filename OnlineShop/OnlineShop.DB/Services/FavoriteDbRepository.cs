using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Services
{
    public class FavoriteDbRepository : IFavoriteRepository
    {
        private readonly DatabaseContext databaseContext;

        public FavoriteDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public void Add(string userId, Item item)
        {
            var addFavoriteItem = TryGetByUserItem(userId, item);
            if (addFavoriteItem == null)
            {
                databaseContext.FavoriteItems.Add(new FavoriteItem
                {
                    UserId = userId,
                    Item = item
                });
                databaseContext.SaveChanges();
            }
        }

        public void Remove(string userId, Item item)
        {
            var removeFavoriteItem = TryGetByUserItem(userId, item);
            databaseContext.Remove(removeFavoriteItem);
            databaseContext.SaveChanges();
        }

        public void Clear(string userId)
        {
            var userFavoriteItems = databaseContext.FavoriteItems
                .Where(x => x.UserId == userId).ToList();
            databaseContext.FavoriteItems.RemoveRange(userFavoriteItems);
            databaseContext.SaveChanges();
        }

        public List<Item> GetAll(string userId)
        {
            var items = databaseContext.FavoriteItems
                .Where(x => x.UserId == userId)
                .Include(x => x.Item)
                .Select(x => x.Item)
                .ToList();
            return items;
        }

        public FavoriteItem TryGetByUserItem(string userId, Item item)
        {
            return databaseContext.FavoriteItems
                .FirstOrDefault(x => x.UserId == userId && x.Item.Id == item.Id);
        }
    }
}