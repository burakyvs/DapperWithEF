using Entity.Abstract;
using System.Data.Common;

namespace DataAccess.Abstract
{
    public abstract class DataAccessorBase<TEntity> : IDataAccessor<TEntity> where TEntity : class, IEntity
    {
        public abstract Task AddAsync(DbTransaction transaction, TEntity entity);
        public abstract Task DeleteAsync(DbTransaction transaction, TEntity entity);
        public abstract Task AddRangeAsync(DbTransaction transaction, ICollection<TEntity> entities);
        public abstract Task DeleteRangeAsync(DbTransaction transaction, ICollection<TEntity> entities);
        public abstract Task UpdateAsync(DbTransaction transaction, TEntity entity);
        public abstract Task<TEntity> GetByIdAsync(DbConnection connection, int id);
        public abstract Task<IEnumerable<TEntity>> GetAllAsync(DbConnection connection, Func<TEntity, bool>? expression = null);
    }
}
