using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShop.Db.Services;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class OrderController : Controller
    {
        private readonly IOrdersRepository ordersRepository;

        public OrderController(IOrdersRepository ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }

        public IActionResult Index()
        {
            var ordersDb = ordersRepository.GetAll();
            var ordersViewModel = ordersDb.ToOrdersViewModel();
            return View(ordersViewModel);
        }

        public IActionResult Details(Guid orderId)
        {
            var orderDb = ordersRepository.TryGetById(orderId);
            var orderViewModel = orderDb.ToOrderViewModel();
            return View(orderViewModel);
        }

        public IActionResult UpdateStatus(Guid orderId, OrderStatusViewModel status)
        {
            ordersRepository.UpdateStatus(orderId, (OrderStatus)(int)status);
            return RedirectToAction(nameof(Index));
        }
    }
}