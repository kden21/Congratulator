using Congratulator.Data.Interfaces;
using Congratulator.Data.Models;

namespace Congratulator.Data.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        public Task<bool> Create(Person entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Person entity)
        {
            throw new NotImplementedException();
        }

        public Task<Person> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<Person>> Select()
        {
            throw new NotImplementedException();
        }

        public Task<Person> Update(Person entity)
        {
            throw new NotImplementedException();
        }
    }
}
