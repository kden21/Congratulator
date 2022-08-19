using Congratulator.Data.Interfaces;
using Congratulator.Data.Models;
using Congratulator.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Congratulator.Data.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        public readonly CongratulatorContext _context;
        public PersonRepository(CongratulatorContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Person entity)
        {
            await _context.Persons.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Person entity)
        {
            _context.Persons.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Person> Get(int id)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(p => p.Id == id);
            return person;
        }

        public async Task<List<Person>> GetByDate(DateTime date)
        {
            var persons = _context.Persons.Where(p => p.DateOfBirth.Month == date.Month).Where(p => p.DateOfBirth.Day == date.Day);
            return await persons.ToListAsync();
        }

        public async Task<List<Person>> Select()
        {
            var persons = await _context.Persons.ToListAsync();
            return persons;
        }

        public async Task<Person> Update(Person entity)
        {
            _context.Persons.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
