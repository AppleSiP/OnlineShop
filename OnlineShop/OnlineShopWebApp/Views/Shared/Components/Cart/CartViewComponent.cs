using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShop.Db.Services;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Views.Shared.ViewComponents.CartViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartsRepository cartsRepository;
        private readonly UserManager<User> usersManager;

        public CartViewComponent(ICartsRepository cartsRepository, UserManager<User> usersManager)
        {
            this.cartsRepository = cartsRepository;
            this.usersManager = usersManager;
        }

        public IViewComponentResult Invoke()
        {

            if (User.Identity.IsAuthenticated)
            {
                var user = usersManager.FindByNameAsync(User.Identity.Name).Result;
                var cartDb = cartsRepository.TryGetByUserId(user.Id);
                var cartViewModel = cartDb.ToCartViewModel();
                var cartAmount = cartViewModel?.Amount ?? 0;
                return View("Cart", cartAmount);
            }
            return View("Cart", 0);
        }
    }
}
