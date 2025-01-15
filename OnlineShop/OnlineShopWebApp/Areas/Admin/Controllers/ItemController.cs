using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Services;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class ItemController : Controller
    {
        private readonly IItemsRepository itemsRepository;

        public ItemController(IItemsRepository itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }

        public IActionResult Index()
        {
            var itemsDb = itemsRepository.GetAll();
            var itemsViewModel = itemsDb.ToItemsViewModel();
            return View(itemsViewModel);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ItemViewModel itemViewModel)
        {
            if (itemViewModel.Cost == 0)
            {
                ModelState.AddModelError("Cost", "Цена должна быть больше 0!");
            }

            if (!ModelState.IsValid)
            {
                return View(itemViewModel);
            }

            var itemDb = itemViewModel.ToItemDbModel();
            itemsRepository.Add(itemDb);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid itemId)
        {
            var itemDb = itemsRepository.TryGetById(itemId);
            var itemViewModel = itemDb.ToItemViewModel();
            return View(itemViewModel);
        }

        [HttpPost]
        public IActionResult Edit(ItemViewModel itemViewModel)
        {
            if (itemViewModel.Cost == 0)
            {
                ModelState.AddModelError("Cost", "Цена должна быть больше 0!");
            }

            if (!ModelState.IsValid)
            {
                return View(itemViewModel);
            }

            var itemDb = itemViewModel.ToItemDbModel();
            itemsRepository.Edit(itemDb);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(Guid itemId)
        {
            var itemDb = itemsRepository.TryGetById(itemId);
            itemsRepository.Remove(itemDb);
            return RedirectToAction(nameof(Index));
        }
    }
}