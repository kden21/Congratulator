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
                //else if ((DateOfBirth.Month == dateNow.Month) && (DateOfBirth.Day == dateNow.Day))
                //{
                //    int age = dateNow.Year - DateOfBirth.Year;
                //    return age;
                //}
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

            //Uri uri = new
            //HttpApplication app = new HttpApplication();
            //var request = HttpContext.Request.Path;
            //var GetDirectory = VirtualPathUtility.GetDirectory(Request.AppRelativeCurrentExecutionFilePath);
            //string basePath = Uri.//Request.Uri.GetLeftPart(UriPartial.Authority) + "/images/Image1.gif"; ; //AppDomain.CurrentDomain.BaseDirectory;//Environment.CurrentDirectory;
           // string relativePath = "~/assets/profile.png";
            //string file_path = Path.Combine(_appEnvironment.ContentRootPath, "Files/book.pdf");
            return "https://localhost:7177/assets/profile.png";// Path.GetFullPath(relativePath, basePath);
        }
       
    }
}
