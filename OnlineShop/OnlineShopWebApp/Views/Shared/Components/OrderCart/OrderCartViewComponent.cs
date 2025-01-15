using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShop.Db.Services;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Views.Shared.Components.OrderCart
{
    public class OrderCartViewComponent : ViewComponent
    {
        private readonly ICartsRepository cartsRepository;
        private readonly UserManager<User> usersManager;

        public OrderCartViewComponent(ICartsRepository cartsRepository, UserManager<User> usersManager)
        {
            this.cartsRepository = cartsRepository;
            this.usersManager = usersManager;
        }

        public IViewComponentResult Invoke()
        {
            var user = usersManager.FindByNameAsync(User.Identity.Name).Result;
            var cartDb = cartsRepository.TryGetByUserId(user.Id);
            var cartViewModel = cartDb.ToCartViewModel();
            return View("OrderCart", cartViewModel);
        }
    }
}