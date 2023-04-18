
using System.ComponentModel.DataAnnotations;

namespace CarWorkShop.Models.ViewModel.Account
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Укажите имя")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [MinLength(5, ErrorMessage = "Пароль должен состоять не менее чем из 5 символов")]
        public string NewPassword { get; set; }
    }
}
