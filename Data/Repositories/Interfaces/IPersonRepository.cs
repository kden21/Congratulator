using Congratulator.Data.Models;

namespace Congratulator.Data.Interfaces
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<List<Person>> GetByName(string name);
        Task<List<Person>> GetByDate(DateTime date);
    }
}
