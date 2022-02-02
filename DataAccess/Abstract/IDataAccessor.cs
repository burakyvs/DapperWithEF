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
        public Task AddAsync(DbTransaction transaction, TEntity entity);
        public Task DeleteAsync(DbTransaction transaction, TEntity entity);
        public Task AddRangeAsync(DbTransaction transaction, ICollection<TEntity> entities);
        public Task DeleteRangeAsync(DbTransaction transaction, ICollection<TEntity> entities);
        public Task UpdateAsync(DbTransaction transaction, TEntity entity);
        public Task<TEntity> GetByIdAsync(DbConnection connection, int id);
        public Task<IEnumerable<TEntity>> GetAllAsync(DbConnection connection);
    }
}
