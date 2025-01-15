using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShop.Db.Services;

namespace OnlineShopWebApp.Views.Shared.ViewComponents.FavoriteItemsCountViewComponent
{
    public class CompareItemsCountViewComponent : ViewComponent
    {
        private readonly ICompareRepository compareRepository;
        private readonly UserManager<User> usersManager;

        public CompareItemsCountViewComponent(ICompareRepository compareRepository, UserManager<User> usersManager)
        {
            this.compareRepository = compareRepository;
            this.usersManager = usersManager;
        }

        public IViewComponentResult Invoke()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = usersManager.FindByNameAsync(User.Identity.Name).Result;
                var itemsCount = compareRepository.GetAll(user.Id).Count;
                return View("CompareItemsCountView", itemsCount);
            }
            return View("CompareItemsCountView", 0);
        }
    }
}
