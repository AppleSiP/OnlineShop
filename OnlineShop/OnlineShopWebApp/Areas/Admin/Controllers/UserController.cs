using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class UserController : Controller
    {
        private readonly UserManager<User> usersManager;

        public UserController(UserManager<User> usersManager)
        {
            this.usersManager = usersManager;
        }

        public IActionResult Index()
        {
            var users = usersManager.Users.ToList();
            var usersViewModel = users.Select(x => x.ToUserViewModel()).ToList();
            return View(usersViewModel);
        }

        public IActionResult Details(string email)
        {
            var userAccount = usersManager.FindByNameAsync(email).Result;
            return View(userAccount.ToUserAccountViewModel());
        }

        public IActionResult Remove(string email)
        {
            var currentUser = usersManager.FindByNameAsync(User.Identity.Name).Result;
            var userAccount = usersManager.FindByNameAsync(email).Result;

            if (userAccount == currentUser)
            {
                usersManager.DeleteAsync(userAccount).Wait();
                return RedirectToAction(nameof(AccountController.Logout), "Account", new { Area = "" });
            }

            usersManager.DeleteAsync(userAccount).Wait();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserCreateViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (usersManager.FindByNameAsync(user.Email).Result != null)
            {
                ModelState.AddModelError("", "Пользователь с таким e-mail уже существует!");
                return View();
            }

            if (usersManager.Users.ToList()
                    .FirstOrDefault(x => x.PhoneNumber == user.Phone) != null)
            {
                ModelState.AddModelError("", "Пользователь с таким номером телефона уже существует!");
                return View();
            }

            if (user.Email == user.Password)
            {
                ModelState.AddModelError("Password", "Логин и пароль не должны совпадать");
            }

            if (ModelState.IsValid)
            {
                var newUser = new User
                {
                    UserName = user.Email,
                    Email = user.Email,
                    PhoneNumber = user.Phone,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
                var result = usersManager.CreateAsync(newUser, user.Password).Result;

                if (result.Succeeded)
                {
                    usersManager.AddToRoleAsync(newUser, "User").Wait();
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(string email)
        {
            var user = usersManager.FindByNameAsync(email).Result;
            var editUser = new UserEditViewModel
            {
                Email = user.Email,
                Phone = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return View(editUser);
        }

        [HttpPost]
        public IActionResult Edit(UserEditViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (user.NewEmail != user.Email && usersManager.FindByNameAsync(user.NewEmail).Result != null)
            {
                ModelState.AddModelError("", "Пользователь с таким e-mail уже существует!");
                return View(user);
            }

            if (user.NewPhone != user.Phone && usersManager.Users.ToList()
                .FirstOrDefault(x => x.PhoneNumber == user.NewPhone) != null)
            {
                ModelState.AddModelError("", "Пользователь с таким номером телефона уже существует!");
                return View(user);
            }

            var currentUser = usersManager.FindByNameAsync(User.Identity.Name).Result;
            var existingUser = usersManager.FindByNameAsync(user.Email).Result;
            existingUser.UserName = user.NewEmail;
            existingUser.Email = user.NewEmail;
            existingUser.PhoneNumber = user.NewPhone;
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            usersManager.UpdateAsync(existingUser).Wait();


            if (existingUser == currentUser && user.NewEmail !=user.Email)
            {
                return RedirectToAction(nameof(AccountController.Logout), "Account", new { Area = ""});
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ChangePassword(string email)
        {
            var user = usersManager.FindByNameAsync(email).Result;
            var changePassword = new ChangePasswordViewModel
            {
                Email = user.Email,
            };
            return View(changePassword);
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel account)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (account.Email == account.Password)
            {
                ModelState.AddModelError("Password", "Логин и пароль не должны совпадать");
            }

            var user = usersManager.FindByNameAsync(account.Email).Result;
            usersManager.RemovePasswordAsync(user);
            usersManager.AddPasswordAsync(user, account.Password);
            return RedirectToAction(nameof(Index));
        }
    }
}