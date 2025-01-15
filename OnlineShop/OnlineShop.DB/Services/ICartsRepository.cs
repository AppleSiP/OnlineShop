using OnlineShop.Db.Models;

namespace OnlineShop.Db.Services
{
    public interface ICartsRepository
    {
        void Add(Item item, string userId);
        void Clear(string userId);
        void Remove(Item item, string userId);
        Cart TryGetByUserId(string userId);
    }
}