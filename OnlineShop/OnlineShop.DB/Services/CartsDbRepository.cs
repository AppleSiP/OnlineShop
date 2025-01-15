using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Services
{
    public class CartsDbRepository : ICartsRepository
    {
        private readonly DatabaseContext databaseContext;

        public CartsDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public Cart TryGetByUserId(string userId)
        {
            return databaseContext.Carts
                .Include(x => x.Items)
                .ThenInclude(x => x.Item)
                .FirstOrDefault(cart => cart.UserId == userId);
        }

        public void Add(Item item, string userId)
        {
            var existingCart = TryGetByUserId(userId);
            if (existingCart == null)
            {
                var newCart = new Cart
                {
                    UserId = userId
                };
                newCart.Items = new List<CartItem>
                {
                    new CartItem
                    {
                        Amount = 1,
                        Item = item,
                    }
                };
                databaseContext.Carts.Add(newCart);
            }
            else
            {
                var existingCartItem = existingCart.Items
                    .FirstOrDefault(cartItem => cartItem.Item.Id == item.Id);
                if (existingCartItem == null)
                {
                    existingCart.Items.Add(new CartItem
                    {
                        Amount = 1,
                        Item = item,
                    });
                }
                else
                {
                    existingCartItem.Amount++;
                }
            }
            databaseContext.SaveChanges();
        }

        public void Remove(Item item, string userId)
        {
            var existingCart = TryGetByUserId(userId);
            var existingCartItem = existingCart.Items
                .FirstOrDefault(cartItem => cartItem.Item.Id == item.Id);
            existingCartItem.Amount--;

            if (existingCartItem.Amount == 0)
            {
                existingCart.Items.Remove(existingCartItem);
            }

            if (existingCart.Items.Count == 0)
            {
                databaseContext.Carts.Remove(existingCart);
            }

            databaseContext.SaveChanges();
        }

        public void Clear(string userId)
        {
            var existingCart = TryGetByUserId(userId);
            databaseContext.Carts.Remove(existingCart);
            databaseContext.SaveChanges();
        }
    }
}