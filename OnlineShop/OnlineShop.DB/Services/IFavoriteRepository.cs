﻿using OnlineShop.Db.Models;

namespace OnlineShop.Db.Services
{
    public interface IFavoriteRepository
    {
        void Add(string userId, Item item);
        void Clear(string userId);
        List<Item> GetAll(string userId);
        void Remove(string userId, Item item);
    }
}