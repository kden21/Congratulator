using Congratulator.Data.Models;
using Congratulator.Data.Models.Enums;

namespace Congratulator.Data.Interfaces
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<List<Person>> GetByName(string name);
        Task<List<Person>> GetByDate(DateTime date);
        Task<List<Person>> Select(StatusSorting statusSorting);
        //Task<bool> Congratulate(int id);
    }
}
