using DAL.Context;
using DAL.Infrastructure.DI.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DAL.Infrastructure.DI.Implementation
{
    public abstract class RepositoryBase<TEntity, TKey>: IRepositoryBase<TEntity, TKey> where TEntity : class
    {
        private readonly DbContext _context;
        public DbSet<TEntity> Table { get; }
        protected RepositoryBase(DogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Table = _context.Set<TEntity>();
        }
        public virtual IEnumerable<TEntity> GetAll()
        {
            return Table;
        }

        public virtual async Task<TEntity?> GetByKeyAsync(TKey key)
        {
            return await Table.FindAsync(key);
        }

        public async Task AddAsync(TEntity entity)
        {
            await Table.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            Table.Update(entity);
            await SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            Table.Remove(entity);
            return await SaveChangesAsync();
        }
        public virtual async Task<int> SaveChangesAsync()
        {
            try
            {
               return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred updating the database", ex);
            }
        }
    }
}
