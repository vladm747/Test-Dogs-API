using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Infrastructure.DI.Abstract
{
    public interface IRepositoryBase<TEntity, TKey>
    {
        IEnumerable<TEntity> GetAll();
        Task<TEntity?> GetByKeyAsync(TKey key);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(TEntity entity);
    }
}
