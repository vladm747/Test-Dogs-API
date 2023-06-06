using DAL.Context;
using DAL.Infrastructure.DI.Abstract;
using DAL.Models;

namespace DAL.Infrastructure.DI.Implementation
{
    public class DogRepository: RepositoryBase<Dog, string>, IDogRepository
    {
        public DogRepository(DogContext context):base(context) { }
    }
}
