using System.ComponentModel.DataAnnotations;

namespace CarWorkShop.Models.ViewModel.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите имя")]
        [MaxLength(20, ErrorMessage = "Логин должен иметь длину не более 20 символов")]
        [MinLength(5, ErrorMessage = "Логин должен иметь длину более 5 символов")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
