using System.ComponentModel.DataAnnotations;

namespace Congratulator.Data.Models.ViewModels
{
    public class EditPersonViewModel
    {
        public Person? Person { get; set; }

        [MaxLength(12, ErrorMessage = "Имя должно иметь длину меньше 12 символов")]
        public string? Name { get; set; }

        [MaxLength(12, ErrorMessage = "Фамилия должна иметь длину меньше 12 символов")]
        public string? Surname { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [MaxLength(4, ErrorMessage = "Год имеет длину 4 символа")]
        [MinLength(4, ErrorMessage = "Год имеет длину 4 символа")]
        public string? YearLastCongratulations { get; set; }

        public IFormFile? Avatar { get; set; }
    }
}
