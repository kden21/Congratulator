using Congratulator.Data.Models;
using Congratulator.Data.Models.Enums;

namespace Congratulator.Data.Interfaces
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<List<Person>> GetByDate(DateTime date);
    }
}
