using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Поле \"E-mail\" не может быть пустым!")]
        [EmailAddress(ErrorMessage = "Введён некорректный email адрес!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле \"Пароль\" не может быть пустым!")]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "Пароль должен содержать от 6 до 32 символов!")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}