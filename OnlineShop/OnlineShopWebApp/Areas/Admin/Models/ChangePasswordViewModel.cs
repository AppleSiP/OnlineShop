using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public class ChangePasswordViewModel
    {
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле \"Пароль\" не может быть пустым!")]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "Пароль должен содержать от 6 до 32 символов!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле \"Повторите пароль\"не может быть пустым!")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
        public string ConfirmPassword { get; set; }
    }
}
