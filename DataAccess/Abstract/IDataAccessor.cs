using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IDataAccessor<TEntity>
    {
        public Task AddAsync(DbConnection connection, TEntity entity);
        public Task DeleteAsync(DbConnection connection, TEntity entity);
        public Task AddRangeAsync(DbConnection connection, ICollection<TEntity> entities);
        public Task DeleteRangeAsync(DbConnection connection, ICollection<TEntity> entities);
        public Task UpdateAsync(DbConnection connection, TEntity entity);
        public Task<TEntity> GetByIdAsync(DbConnection connection, int id);
        public Task<IEnumerable<TEntity>> GetAllAsync(DbConnection connection);
    }
}
