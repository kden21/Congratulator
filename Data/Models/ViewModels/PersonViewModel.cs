using System.ComponentModel.DataAnnotations;

namespace Congratulator.Data.Models.ViewModels
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите имя")]
        [MaxLength(12, ErrorMessage = "Имя должно иметь длину меньше 12 символов")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Введите фамилию")]
        [MaxLength(12, ErrorMessage = "Имя должно иметь длину меньше 12 символов")]
        public string? Surname { get; set; }

        [Required(ErrorMessage = "Введите дату рождения")]
        public DateTime DateOfBirth { get; set; } 
    }
}
