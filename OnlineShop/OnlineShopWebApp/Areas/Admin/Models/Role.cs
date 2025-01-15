using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public class Role
    {
        [Required(ErrorMessage = "Поле не может быть пустым!")]
        public string Name { get; set; }
    }
}