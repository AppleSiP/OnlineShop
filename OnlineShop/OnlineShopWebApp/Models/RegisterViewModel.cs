using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Поле \"E-mail\" не может быть пустым!")]
        [EmailAddress(ErrorMessage = "Введён некорректный email адрес!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле \"Телефон\" не может быть пустым!")]
        [StringLength(16, MinimumLength = 10, ErrorMessage = "требуется от 10 до 16 символов!")]
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Недопустимый формат записи")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Поле \"Пароль\" не может быть пустым!")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\S{6,32}$",
            ErrorMessage = "Пароли должны содержать хотя бы один не буквенно-цифровой символ." +
            "\r\nПароли должны иметь хотя бы одну латинскую строчную букву ('a'-'z')." +
            "\r\nПароли должны иметь хотя бы одну латинскую заглавную букву ('A'-'Z')." +
            "\r\nПароли должны иметь хотя бы одну цифру." +
            "\r\nПароль должен содержать от 6 до 32 символов.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле \"Повторите пароль\"не может быть пустым!")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Имя не может быть пустым!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилия не может быть пустой!")]
        public string LastName { get; set; }

        public string ReturnUrl { get; set; }
    }
}