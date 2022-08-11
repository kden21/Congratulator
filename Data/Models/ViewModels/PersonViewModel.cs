using System.ComponentModel.DataAnnotations;
using System.Drawing;
//using static System.Net.Mime.MediaTypeNames;
//using System.Drawing;

namespace Congratulator.Data.Models.ViewModels
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите имя")]
        [MaxLength(12, ErrorMessage = "Имя должно иметь длину меньше 12 символов")]
        public string Name { get; set; } = null!;

        //[Required(ErrorMessage = "Введите фамилию")]
        [MaxLength(12, ErrorMessage = "Имя должно иметь длину меньше 12 символов")]
        public string? Surname { get; set; }

        [Required(ErrorMessage = "Введите дату рождения")]
        public DateTime DateOfBirth { get; set; }
        public IFormFile? Avatar { get; set; }
        

        /*public string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                //format = image.f
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to base 64 string
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
        public Image Base64ToImage(string base64String)
        {
            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                Image image = Image.FromStream(ms, true);
                System.Drawing.Imaging.ImageFormat format = image.RawFormat;
                return image;
            }
        }*/
    }
}
