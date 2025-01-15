using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShop.Db.Services;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ICartsRepository cartsRepository;
        private readonly IOrdersRepository ordersRepository;
        private readonly UserManager<User> usersManager;
        public OrderController(ICartsRepository cartsRepository, IOrdersRepository ordersRepository, UserManager<User> usersManager)
        {
            this.cartsRepository = cartsRepository;
            this.ordersRepository = ordersRepository;
            this.usersManager = usersManager;
        }

        public IActionResult Index()
        {
            var user = usersManager.FindByNameAsync(User.Identity.Name).Result;
            var existingCart = cartsRepository.TryGetByUserId(user.Id);
            var cartViewModel = existingCart.ToCartViewModel();

            if (cartViewModel == null || cartViewModel.Amount == 0)
            {
                return View("ErrorEmptyOrder");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Buy(UserDeliveryInfoViewModel userDeliveryInfoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", userDeliveryInfoViewModel);
            }
            var user = usersManager.FindByNameAsync(User.Identity.Name).Result;
            var existingCart = cartsRepository.TryGetByUserId(user.Id);

            if (existingCart != null && existingCart.Items.Count != 0)
            {
                var orderDb = new Order();
                orderDb.UserDeliveryInfo = userDeliveryInfoViewModel.ToUserDeliveryInfoDbModel();
                orderDb.Items = new List<CartItem>();

                foreach (var cartItem in existingCart.Items)
                {
                    orderDb.Items.Add(cartItem);
                }

                ordersRepository.Add(orderDb);
                cartsRepository.Clear(user.Id);
                return View();
            }
            return RedirectToAction("Index");
        }
    }
}