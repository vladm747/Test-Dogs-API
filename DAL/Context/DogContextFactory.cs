using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DAL.Context
{
    public class DogContextFactory : IDesignTimeDbContextFactory<DogContext>
    {
        public DogContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DogContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=DogsDB;Trusted_Connection=true;TrustServerCertificate=true;");
            return new DogContext(optionsBuilder.Options);
        }
    }
}
