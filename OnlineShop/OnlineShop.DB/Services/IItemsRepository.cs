using OnlineShop.Db.Models;

namespace OnlineShop.Db.Services
{
    public interface IItemsRepository
    {
        List<Item> GetAll();
        Item TryGetById(Guid id);
        void Add(Item item);
        void Edit(Item item);
        void Remove(Item item);
    }
}