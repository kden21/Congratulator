using Congratulator.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Congratulator.Data
{
    public class CongratulatorContext : DbContext
    {
        public CongratulatorContext(DbContextOptions<CongratulatorContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Person> Persons { get; set; } = null!;
    }
}
