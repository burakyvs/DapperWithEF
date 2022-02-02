using Dapper.Contrib.Extensions;
using DataAccess.Abstract;
using System.Data.Common;

namespace DataAccess.Concrete.Dapper
{
    public class DapperAccessor<TEntity> : DataAccessorBase<TEntity> where TEntity : class
    {
        public DapperAccessor()
        {
        }

        public async override Task AddAsync(DbConnection connection, TEntity entity)
        {
            await connection.InsertAsync(entity);
        }

        public override async Task AddRangeAsync(DbConnection connection, ICollection<TEntity> entities)
        {
            await connection.InsertAsync(entities);
        }

        public override async Task DeleteAsync(DbConnection connection, TEntity entity)
        {
            await connection.DeleteAsync(entity);
        }

        public override async Task DeleteRangeAsync(DbConnection connection, ICollection<TEntity> entities)
        {
            await connection.DeleteAsync(entities);
        }

        public override async Task<IEnumerable<TEntity>> GetAllAsync(DbConnection connection)
        {
            var entities = await connection.GetAllAsync<TEntity>();
            return entities;
        }

        public override async Task<TEntity> GetByIdAsync(DbConnection connection, int id)
        {
            var entity = await connection.GetAsync<TEntity>(id);
            return entity;
        }

        public override async Task UpdateAsync(DbConnection connection, TEntity entity)
        {
             await connection.UpdateAsync(entity);
        }
    }
}
