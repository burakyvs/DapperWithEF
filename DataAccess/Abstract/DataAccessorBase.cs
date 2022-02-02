using DataAccess.EntityFramework.Contexts;
using Npgsql;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public abstract class DataAccessorBase<TEntity> : IDataAccessor<TEntity> where TEntity : class
    {
        public abstract Task AddAsync(DbConnection connection, TEntity entity);
        public abstract Task DeleteAsync(DbConnection connection, TEntity entity);
        public abstract Task AddRangeAsync(DbConnection connection, ICollection<TEntity> entities);
        public abstract Task DeleteRangeAsync(DbConnection connection, ICollection<TEntity> entities);
        public abstract Task UpdateAsync(DbConnection connection, TEntity entity);
        public abstract Task<TEntity> GetByIdAsync(DbConnection connection, int id);
        public abstract Task<IEnumerable<TEntity>> GetAllAsync(DbConnection connection);
    }
}
