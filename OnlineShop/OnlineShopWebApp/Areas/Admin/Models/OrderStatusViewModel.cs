using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public enum OrderStatusViewModel
    {
        [Display(Name = "Ожидает подтверждения")]
        Confirmation,

        [Display(Name = "Создан")]
        Created,

        [Display(Name = "На комплектовке")]
        Assembly,

        [Display(Name = "В доставке")]
        Delivering,

        [Display(Name = "На хранении")]
        Storage,

        [Display(Name = "Завершен")]
        Finished,

        [Display(Name = "Отменен")]
        Canceled
    }
}