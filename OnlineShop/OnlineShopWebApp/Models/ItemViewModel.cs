using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class ItemViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Поле \"Наименование\" не может быть пустым!")]
        [StringLength(32, MinimumLength = 3, ErrorMessage = "Наименование должно содержать от 3 до 32 символов!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле \"Цена\" не может быть пустым!")]
        [RegularExpression(@"^[0-9]{1,7}(?:[,][0-9]{1,2})?$", ErrorMessage = "Недопустимый формат записи - введите положительное число или используйте в качестве разделителя запятую ','; Максимальное значение не должно превышать 9999999,99")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Поле \"Материал\" не может быть пустым!")]
        [StringLength(6, MinimumLength = 3, ErrorMessage = "От 3 до 6 символов!")]
        public string Material { get; set; }

        [Required(ErrorMessage = "Поле \"Описание\" не может быть пустым!")]
        [StringLength(64, MinimumLength = 4, ErrorMessage = "От 4 до 64 символов!")]
        public string Description { get; set; }
        public string ImagePath { get; set; } = "/images/noimage.png";

    }
}