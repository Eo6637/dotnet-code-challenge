using CodeChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Data
{
    public class CompContext : DbContext
    {
        public CompContext(DbContextOptions<CompContext> options) : base(options) 
        {

        }

        public DbSet<Compensation> Compensations { get; set; }
    }
}
