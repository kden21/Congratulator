using System.ComponentModel.DataAnnotations;
using System.Drawing;
//using static System.Net.Mime.MediaTypeNames;
//using System.Drawing;

namespace Congratulator.Data.Models.ViewModels
{
    public class PersonViewModel
    {
        public Person? Person { get; set; }
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [MaxLength(12, ErrorMessage = "Имя должно иметь длину меньше 12 символов")]
        public string Name { get; set; } = null!;

        [MaxLength(12, ErrorMessage = "Имя должно иметь длину меньше 12 символов")]
        public string? Surname { get; set; }

        [Required(ErrorMessage = "Введите дату рождения")]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Неккоректный номер телефона")]
        public string? PhoneNumber { get; set; }
        public IFormFile? Avatar { get; set; }
    }
}
