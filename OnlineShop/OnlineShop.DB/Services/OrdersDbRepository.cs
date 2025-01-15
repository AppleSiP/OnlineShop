using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Services
{
    public class OrdersDbRepository : IOrdersRepository
    {
        private readonly DatabaseContext databaseContext;

        public OrdersDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public void Add(Order order)
        {
            databaseContext.Orders.Add(order);
            databaseContext.SaveChanges();
        }

        public List<Order> GetAll()
        {
            var orders = databaseContext.Orders
                .Include(x => x.UserDeliveryInfo)
                .Include(x => x.Items)
                .ThenInclude(x => x.Item)
                .ToList();
            return orders;
        }

        public Order TryGetById(Guid id)
        {
            var order = databaseContext.Orders
                .Include(x => x.UserDeliveryInfo)
                .Include(x => x.Items)
                .ThenInclude(x => x.Item)
                .FirstOrDefault(x => x.Id == id);
            return order;
        }

        public void UpdateStatus(Guid orderId, OrderStatus newStatus)
        {
            var order = TryGetById(orderId);
            if (order != null)
            {
                order.Status = newStatus;
            }
            databaseContext.SaveChanges();
        }
    }
}