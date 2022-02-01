using Dapper;
using Dapper.Contrib.Extensions;
using DataAccess.Abstract;
using System.Linq.Expressions;

namespace DataAccess.Concrete.Dapper
{
    public class DapperAccessor<TEntity> : DataAccessorBase<TEntity> where TEntity : class
    {
        public DapperAccessor(string connectionString) : base(connectionString)
        {
        }

        public async override void AddAsync(TEntity entity)
        {
            using (var connection = await base.OpenConnectionAsync())
            {
                using (var transaction = await base.BeginTransaction(connection))
                {
                    connection.Insert(entity);
                }
            }
        }

        public override async void AddRangeAsync(ICollection<TEntity> entities)
        {
            using (var connection = await base.OpenConnectionAsync())
            {
                using (var transaction = await base.BeginTransaction(connection))
                {
                    await connection.InsertAsync(entities);
                }
            }
        }

        public override async void DeleteAsync(TEntity entity)
        {
            using (var connection = await base.OpenConnectionAsync())
            {
                using (var transaction = await base.BeginTransaction(connection))
                {
                    await connection.DeleteAsync(entity);
                }
            }
        }

        public override async void DeleteRangeAsync(ICollection<TEntity> entities)
        {
            using (var connection = await base.OpenConnectionAsync())
            {
                using (var transaction = await base.BeginTransaction(connection))
                {
                    await connection.DeleteAsync(entities);
                }
            }
        }

        public override async void GetAllAsync(Expression expression)
        {
            using (var connection = await base.OpenConnectionAsync())
            {
                using (var transaction = await base.BeginTransaction(connection))
                {
                    var entities = await connection.GetAllAsync<TEntity>();
                }
            }
        }

        public override async void GetByIdAsync(int id)
        {
            using (var connection = await base.OpenConnectionAsync())
            {
                using (var transaction = await base.BeginTransaction(connection))
                {
                    var entities = await connection.GetAsync<TEntity>(id);
                }
            }
        }

        public override async void UpdateAsync(TEntity entity)
        {
            using (var connection = await base.OpenConnectionAsync())
            {
                using (var transaction = await base.BeginTransaction(connection))
                {
                    await connection.UpdateAsync(entity);
                }
            }
        }
    }
}
