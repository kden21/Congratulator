namespace Congratulator.Data.Models
{
    public class Person
    {
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? YearLastCongratulations { get; set; }
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
    }
}
