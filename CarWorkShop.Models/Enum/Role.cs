using System.ComponentModel.DataAnnotations;

namespace CarWorkShop.Models.Enum
{
    public enum Role
    {
        [Display(Name = "Пользователь")]
        Owner = 0,
        [Display(Name = "Администратор")]
        Admin = 1,
    }
}
