using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Infrastructure.DI.Abstract;
using DAL.Infrastructure.DI.Implementation;
using DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace DAL.Infrastructure.DI.Implementation
{
    public class DogRepository: RepositoryBase<Dog, string>, IDogRepository
    {
    }
}
