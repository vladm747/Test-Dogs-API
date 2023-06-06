using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class DogContext: DbContext
    {
        public DogContext(DbContextOptions<DogContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Dog> Dogs { get; set; }
    }
}
