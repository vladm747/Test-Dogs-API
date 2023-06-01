using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Infrastructure.DI.Abstract;
using DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace DAL.Infrastructure.DI.Implementation
{
    public abstract class RepositoryBase<TEntity, TKey>: IRepositoryBase<TEntity, TKey>
    {
        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetByKeyAsync(TKey key)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
