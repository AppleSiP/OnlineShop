using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShop.Db.Services;

namespace OnlineShopWebApp.Views.Shared.ViewComponents.FavoriteItemsCountViewComponent
{
    public class FavoriteItemsCountViewComponent : ViewComponent
    {
        private readonly IFavoriteRepository favoriteRepository;
        private readonly UserManager<User> usersManager;

        public FavoriteItemsCountViewComponent(IFavoriteRepository favoriteRepository, UserManager<User> usersManager)
        {
            this.favoriteRepository = favoriteRepository;
            this.usersManager = usersManager;
        }

        public IViewComponentResult Invoke()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = usersManager.FindByNameAsync(User.Identity.Name).Result;
                var itemsCount = favoriteRepository.GetAll(user.Id).Count;
                return View("FavoriteItemsCountView", itemsCount);
            }
            return View("FavoriteItemsCountView", 0);
        }
    }
}
