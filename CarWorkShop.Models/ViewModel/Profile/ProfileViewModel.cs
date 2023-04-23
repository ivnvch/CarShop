using System.ComponentModel.DataAnnotations;

namespace CarWorkShop.Models.ViewModel.Profile
{
    public class ProfileViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите вашу фамилию")]
        [MaxLength(30, ErrorMessage = "Максимальная длина фамилии может достигать 30 символов")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Укажите ваше имя")]
        [MaxLength(20, ErrorMessage = "Максимальная длина имени может достигать 20 символов")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Укажите ваш возраст")]
        [Range(1, 125, ErrorMessage = "Диапазон возраста: 0 - 150")]
        public short Age { get; set; }

        public string? MiddleName { get; set; }

        public string? Login { get; set; }
    }
}
