using System.ComponentModel.DataAnnotations;

namespace Congratulator.Data.Models.ViewModels
{
    public class EditPersonViewModel
    {
        public Person? Person { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [MaxLength(12, ErrorMessage = "Имя должно иметь длину меньше 12 символов")]
        public string? Name { get; set; }

        [MaxLength(12, ErrorMessage = "Фамилия должна иметь длину меньше 12 символов")]
        public string? Surname { get; set; }

        [Required(ErrorMessage = "Введите дату рождения")]
        public DateTime DateOfBirth { get; set; }

        [MaxLength(4, ErrorMessage = "Год имеет длину 4 символа")]
        [MinLength(4, ErrorMessage = "Год имеет длину 4 символа")]
        public string? YearLastCongratulations { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Неккоректный номер телефона")]
        public string? PhoneNumber { get; set; }
        public IFormFile? Avatar { get; set; }
    }
}
