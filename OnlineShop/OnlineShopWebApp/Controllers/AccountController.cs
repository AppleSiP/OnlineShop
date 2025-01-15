using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> usersManager;
        private readonly SignInManager<User> signInManager;


        public AccountController(UserManager<User> usersManager, SignInManager<User> signInManager)
        {
            this.usersManager = usersManager;
            this.signInManager = signInManager;
        }

        public IActionResult Login(string? returnUrl)
        {
            return View(new LoginViewModel() { ReturnUrl = returnUrl ?? "/home/index" });
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            var checkUserName = usersManager.FindByNameAsync(login.Email).Result;

            if (checkUserName == null)
            {
                ModelState.AddModelError("", "Пользователь с таким Email не зарегистрирован");
            }

            if (ModelState.IsValid)
            {
                var result = signInManager.PasswordSignInAsync(login.Email, login.Password,
                    login.RememberMe, false).Result;

                if (result.Succeeded)
                {
                    return Redirect(login.ReturnUrl);
                }

                ModelState.AddModelError("", "Неверный пароль!");
            }

            return View(login);
        }

        public IActionResult Logout()
        {
            signInManager.SignOutAsync().Wait();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult Register(string returnUrl)
        {
            return View(new RegisterViewModel() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerUser)
        {
            if (usersManager.FindByNameAsync(registerUser.Email).Result != null)
            {
                ModelState.AddModelError("", "Пользователь с таким e-mail уже существует!");
            }

            if (usersManager.Users.ToList()
                    .FirstOrDefault(x => x.PhoneNumber == registerUser.Phone) != null)
            {
                ModelState.AddModelError("", "Пользователь с таким номером телефона уже существует!");
            }

            if (registerUser.Email == registerUser.Password)
            {
                ModelState.AddModelError("Password", "Логин и пароль не должны совпадать");
            }

            if (ModelState.IsValid)
            {
                var newUser = new User
                {
                    UserName = registerUser.Email,
                    Email = registerUser.Email,
                    PhoneNumber = registerUser.Phone,
                    FirstName = registerUser.FirstName,
                    LastName = registerUser.LastName
                };
                var result = usersManager.CreateAsync(newUser, registerUser.Password).Result;

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
            return View(registerUser);
        }
    }
}