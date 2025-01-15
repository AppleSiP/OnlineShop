using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Services;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IItemsRepository itemsRepository;
        public HomeController(IItemsRepository itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }

        public IActionResult Index()
        {
            var itemsDb = itemsRepository.GetAll();
            var itemsViewModel = itemsDb.ToItemsViewModel();
            return View(itemsViewModel);
        }
    }
}