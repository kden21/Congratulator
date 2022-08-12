using Azure.Core;
using System.Runtime;
using System.Web;
namespace Congratulator.Data.Models
{
    public class Person
    {
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? YearLastCongratulations { get; set; }
        public byte[]? Avatar { get; set; }
        public int Age(DateTime dateNow)
        {
            if(DateOfBirth.Year < dateNow.Year)
            {
                if ((DateOfBirth.Month<dateNow.Month)||((DateOfBirth.Day < dateNow.Day)&&(DateOfBirth.Month == dateNow.Month)))
                {
                    int age = dateNow.Year - DateOfBirth.Year + 1;
                    return age;
                }
                else
                {
                    int age = dateNow.Year - DateOfBirth.Year;
                    return age;
                }
            }
            else
            {
                return -1;
            }
        }

        public string AvatarUrl()
        {
            if (Avatar != null)
            {
                return "data:image/png;base64," + Convert.ToBase64String(Avatar);
            }
            return "https://localhost:7177/assets/profile.png";// Path.GetFullPath(relativePath, basePath);
        }
    }
}
