using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShop.Db.Services;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class CompareController : Controller
    {
        private readonly ICompareRepository compareRepository;
        private readonly IItemsRepository itemsRepository;
        private readonly UserManager<User> usersManager;

        public CompareController(ICompareRepository compareRepository, IItemsRepository itemsRepository, UserManager<User> usersManager)
        {
            this.compareRepository = compareRepository;
            this.itemsRepository = itemsRepository;
            this.usersManager = usersManager;
        }

        public IActionResult Index()
        {
            var user = usersManager.FindByNameAsync(User.Identity.Name).Result;
            var items = compareRepository.GetAll(user.Id);
            var itemsViewModel = items.ToItemsViewModel();
            return View(itemsViewModel);
        }

        public IActionResult Add(Guid itemId)
        {
            var user = usersManager.FindByNameAsync(User.Identity.Name).Result;
            var item = itemsRepository.TryGetById(itemId);
            compareRepository.Add(user.Id, item);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(Guid itemId)
        {
            var user = usersManager.FindByNameAsync(User.Identity.Name).Result;
            var item = itemsRepository.TryGetById(itemId);
            compareRepository.Remove(user.Id, item);
            return RedirectToAction(nameof(Index));
        }
    }
}
