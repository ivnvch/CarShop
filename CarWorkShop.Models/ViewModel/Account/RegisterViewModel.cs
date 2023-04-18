using System.ComponentModel.DataAnnotations;

namespace CarWorkShop.Models.ViewModel.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Укажите логин")]
        [MaxLength(20, ErrorMessage = "Логин должен иметь длину не более 20 символов")]
        [MinLength(5, ErrorMessage = "Логин должен иметь длину более 5 символов")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Укажите пароль")]
        [MinLength(6, ErrorMessage = "Длина пароля должна быть более 6 символов")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }
    }
}
