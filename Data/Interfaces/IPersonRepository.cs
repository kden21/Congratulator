using Congratulator.Data.Models;

namespace Congratulator.Data.Interfaces
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<Person> GetByName(string name);
    }
}
