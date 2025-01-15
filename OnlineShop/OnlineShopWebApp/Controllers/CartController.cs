using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShop.Db.Services;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartsRepository cartsRepository;
        private readonly IItemsRepository itemsRepository;
        private readonly UserManager<User> usersManager;

        public CartController(ICartsRepository cartsRepository, IItemsRepository itemsRepository, UserManager<User> usersManager)
        {
            this.cartsRepository = cartsRepository;
            this.itemsRepository = itemsRepository;
            this.usersManager = usersManager;
        }

        public IActionResult Index()
        {
            var user = usersManager.FindByNameAsync(User.Identity.Name).Result;
            var cart = cartsRepository.TryGetByUserId(user.Id);
            var cartViewModel = cart.ToCartViewModel();
            return View(cartViewModel);
        }

        public IActionResult Add(Guid itemId)
        {
            var user = usersManager.FindByNameAsync(User.Identity.Name).Result;
            var itemDb = itemsRepository.TryGetById(itemId);
            cartsRepository.Add(itemDb, user.Id);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(Guid itemId)
        {
            var user = usersManager.FindByNameAsync(User.Identity.Name).Result;
            var itemDb = itemsRepository.TryGetById(itemId);
            cartsRepository.Remove(itemDb, user.Id);
            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            var user = usersManager.FindByNameAsync(User.Identity.Name).Result;
            cartsRepository.Clear(user.Id);
            return RedirectToAction("Index");
        }
    }
}