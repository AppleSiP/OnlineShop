using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Services;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IItemsRepository itemsRepository;
        public ProductController(IItemsRepository itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }

        public IActionResult Index(Guid id)
        {
            var itemDb = itemsRepository.TryGetById(id);
            var itemViewModel = itemDb.ToItemViewModel();
            return View(itemViewModel);
        }
    }
}