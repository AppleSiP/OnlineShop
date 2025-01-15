using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Helpers
{
    public static class Mapping
    {
        public static Item ToItemDbModel(this ItemViewModel itemViewModel)
        {
            var itemDb = new Item
            {
                Id = itemViewModel.Id,
                Name = itemViewModel.Name,
                Cost = itemViewModel.Cost,
                Description = itemViewModel.Description,
                Material = itemViewModel.Material,
                ImagePath = itemViewModel.ImagePath
            };
            return itemDb;
        }

        public static ItemViewModel ToItemViewModel(this Item itemDb)
        {
            var itemViewModel = new ItemViewModel
            {
                Id = itemDb.Id,
                Name = itemDb.Name,
                Cost = itemDb.Cost,
                Description = itemDb.Description,
                Material = itemDb.Material,
                ImagePath = itemDb.ImagePath
            };
            return itemViewModel;
        }

        public static List<ItemViewModel> ToItemsViewModel(this List<Item> itemsDb)
        {
            var itemsViewModel = new List<ItemViewModel>();

            foreach (var itemDb in itemsDb)
            {
                var itemViewModel = itemDb.ToItemViewModel();
                itemsViewModel.Add(itemViewModel);
            }
            return itemsViewModel;
        }

        public static CartViewModel ToCartViewModel(this Cart cartDb)
        {
            if (cartDb == null)
            {
                return null;
            }
            var cartViewModel = new CartViewModel
            {
                Id = cartDb.Id,
                UserId = cartDb.UserId,
                Items = cartDb.Items.ToCartItemsViewModel()
            };
            return cartViewModel;
        }

        public static List<CartItemViewModel> ToCartItemsViewModel(this List<CartItem> cartItemsDb)
        {
            var cartItemsViewModel = new List<CartItemViewModel>();

            foreach (var cartItemDb in cartItemsDb)
            {
                var cartItemViewModel = cartItemDb.ToCartItemViewModel();
                cartItemsViewModel.Add(cartItemViewModel);
            }
            return cartItemsViewModel;
        }

        public static CartItemViewModel ToCartItemViewModel(this CartItem cartItemDb)
        {
            var cartItemViewModel = new CartItemViewModel
            {
                Id = cartItemDb.Id,
                Item = cartItemDb.Item.ToItemViewModel(),
                Amount = cartItemDb.Amount
            };
            return cartItemViewModel;
        }

        public static OrderViewModel ToOrderViewModel(this Order orderDb)
        {
            var orderViewModel = new OrderViewModel
            {
                Id = orderDb.Id,
                UserDeliveryInfo = orderDb.UserDeliveryInfo.ToUserDeliveryInfoViewModel(),
                Status = (OrderStatusViewModel)(int)orderDb.Status,
                Items = orderDb.Items.ToCartItemsViewModel(),
                CreatedDataTime = orderDb.CreatedDataTime
            };
            return orderViewModel;
        }

        public static List<OrderViewModel> ToOrdersViewModel(this List<Order> ordersDb)
        {
            var ordersViewModel = new List<OrderViewModel>();

            foreach (var orderDb in ordersDb)
            {
                var orderViewModel = orderDb.ToOrderViewModel();
                ordersViewModel.Add(orderViewModel);
            };
            return ordersViewModel;
        }

        public static UserDeliveryInfo ToUserDeliveryInfoDbModel(this UserDeliveryInfoViewModel userDeliveryInfoViewModel)
        {
            var UserDeliveryInfoDb = new UserDeliveryInfo
            {
                Name = userDeliveryInfoViewModel.Name,
                Address = userDeliveryInfoViewModel.Address,
                Phone = userDeliveryInfoViewModel.Phone
            };
            return UserDeliveryInfoDb;
        }

        public static UserDeliveryInfoViewModel ToUserDeliveryInfoViewModel(this UserDeliveryInfo userDeliveryInfoDb)
        {
            var userDeliveryInfoViewModel = new UserDeliveryInfoViewModel
            {
                Name = userDeliveryInfoDb.Name,
                Address = userDeliveryInfoDb.Address,
                Phone = userDeliveryInfoDb.Phone
            };
            return userDeliveryInfoViewModel;
        }

        public static UserViewModel ToUserViewModel(this User user)
        {
            return new UserViewModel
            {
                Email = user.Email,
                Phone = user.PhoneNumber
            };
        }

        public static UserAccountViewModel ToUserAccountViewModel(this User user)
        {
            return new UserAccountViewModel
            {
                Email = user.Email,
                Phone = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}