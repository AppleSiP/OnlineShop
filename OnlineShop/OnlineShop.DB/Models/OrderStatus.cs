using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Db.Models
{
    public enum OrderStatus
    {
        Confirmation,
        Created,
        Assembly,
        Delivering,
        Storage,
        Finished,
        Canceled
    }
}