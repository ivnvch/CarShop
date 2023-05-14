using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarWorkShop.Models.ViewModel.Record
{
    public class RecordViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Укажите вашу фамилию")]
        [MinLength(2, ErrorMessage = "Минимальная длина фамилии должна составлять 2 символа")]
        [MaxLength(35, ErrorMessage = "Длина фамилии не должна превышать 35 символов")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Укажите ваше имя")]
        [MinLength(2,ErrorMessage ="Минимальная длина имени должна составлять 2 символа")]
        [MaxLength(25, ErrorMessage ="Длина имени не должна превышать 25 символов")]
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }
        [Range(1,125, ErrorMessage ="Диапазон возраста: 1-125")]
        public short Age { get; set; }

        public string? DateTime { get; set; }
        public string? Complaint { get; set; } //жалобы 
        public string Mark { get; set; }
        public string Model { get; set; }
      //  [RegularExpression("d{4}[A-Z]{2}$", ErrorMessage = "Номер автомобиля должен быть в формате AB 1234CD")]
        public string CarNumber { get; set; }
        public IFormFile? Avatar { get; set; }
        public byte[]? Image { get; set; }
        public string? Login { get; set; }
    }
}
