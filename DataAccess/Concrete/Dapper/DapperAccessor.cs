using Dapper.Contrib.Extensions;
using DataAccess.Abstract;
using Entity.Abstract;
using System.Data.Common;

namespace DataAccess.Concrete.Dapper
{
    public class DapperAccessor<TEntity> : DataAccessorBase<TEntity> where TEntity : class, IEntity
    {
        public DapperAccessor()
        {
        }

        public async override Task AddAsync(DbTransaction transaction, TEntity entity)
        {
            await transaction.Connection.InsertAsync(entity, transaction);
        }

        public override async Task AddRangeAsync(DbTransaction transaction, ICollection<TEntity> entities)
        {
            await transaction.Connection.InsertAsync(entities, transaction);
        }

        public override async Task DeleteAsync(DbTransaction transaction, TEntity entity)
        {
            await transaction.Connection.DeleteAsync(entity, transaction);
        }

        public override async Task DeleteRangeAsync(DbTransaction transaction, ICollection<TEntity> entities)
        {
            await transaction.Connection.DeleteAsync(entities);
        }

        public override async Task<IEnumerable<TEntity>> GetAllAsync(DbConnection connection, Func<TEntity, bool> expression = null)
        {
            var entities = await connection.GetAllAsync<TEntity>();
            var queriedEntities = expression != null ? entities.Where(expression) : entities;
            return queriedEntities;
        }

        public override async Task<TEntity> GetByIdAsync(DbConnection connection, int id)
        {
            var entity = await connection.GetAsync<TEntity>(id);
            return entity;
        }

        public override async Task UpdateAsync(DbTransaction transaction, TEntity entity)
        {
             await transaction.Connection.UpdateAsync(entity, transaction);
        }
    }
}
