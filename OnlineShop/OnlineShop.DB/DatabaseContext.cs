using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Item> Items { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<FavoriteItem> FavoriteItems { get; set; }

        public DbSet<CompareItem> CompareItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasData(

                new Item { Id = Guid.NewGuid(), Name = "Шестерня", Cost = 10.52m, Material = "ABS", Description = "Шестерня блендера XS250", ImagePath = "/images/image0.png" },
                new Item { Id = Guid.NewGuid(), Name = "Вал", Cost = 17.99m, Material = "ABS", Description = "Вал приводной блендера XS250", ImagePath = "/images/image1.png" },
                new Item { Id = Guid.NewGuid(), Name = "Крышка", Cost = 2.99m, Material = "PETG", Description = "Крышка контейнера", ImagePath = "/images/image2.png" },
                new Item { Id = Guid.NewGuid(), Name = "Шестерня ведомая", Cost = 20.12m, Material = "PLA", Description = "Ведомая шестерня блендера SX250", ImagePath = "/images/image3.png" },
                new Item { Id = Guid.NewGuid(), Name = "Ось", Cost = 7.60m, Material = "ABS", Description = "Ось ведомой шестерни блендера SX250", ImagePath = "/images/image4.png" }
            );
        }
    }
}