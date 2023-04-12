using System.ComponentModel.DataAnnotations;

namespace CarWorkShop.Models.ViewModel.Owner
{
    public class OwnerViewModel
    {

        [Display(Name = "Id")]
        public long Id { get; set; }

        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Введите логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Укажите роль")]
        [Display(Name = "Роль")]
        public string Role { get; set; }
    }
}
