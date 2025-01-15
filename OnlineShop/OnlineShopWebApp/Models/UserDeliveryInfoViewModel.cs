using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class UserDeliveryInfoViewModel
    {
        [Required(ErrorMessage = "Укажите Ф.И.О.!")]
        [StringLength(64, MinimumLength = 6, ErrorMessage = "Не менее 6, и не более 64 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите адресс!")]
        [StringLength(64, MinimumLength = 6, ErrorMessage = "Не менее 6, и не более 64 символов")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Укажите номер телефона!")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Введите номер телефона в 11-значном формате")]
        public string Phone { get; set; }
    }
}