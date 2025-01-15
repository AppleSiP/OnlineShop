using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class UserEditViewModel
    {
        public string Email { get; set; }

        public string? Phone { get; set; }

        [Required(ErrorMessage = "Поле \"E-mail\" не может быть пустым!")]
        [EmailAddress(ErrorMessage = "Введён некорректный email адрес!")]
        public string NewEmail { get; set; }

        [Required(ErrorMessage = "Поле \"Телефон\" не может быть пустым!")]
        [StringLength(16, MinimumLength = 10, ErrorMessage = "требуется от 10 до 16 символов!")]
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Недопустимый формат записи")]
        public string NewPhone { get; set; }

        [Required(ErrorMessage = "Имя не может быть пустым!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилия не может быть пустой!")]
        public string LastName { get; set; }
    }
}
