using DataAccess.EntityFramework.Contexts;
using Npgsql;
using System.Data.Common;

namespace DataAccess.Abstract
{
    public abstract class DbConnectorBase : IDbConnector
    {
        public string ConnectionString { get => _connectionString; set => _connectionString = value; }
        private string _connectionString;

        public bool IsTransactionActive { get => _isTransactionActive; set => _isTransactionActive = value; }
        private bool _isTransactionActive = false;

        public DbConnectorBase()
        {
            _connectionString = DbContextBase.ConnectionString;
        }

        public abstract Task<DbConnection> OpenConnectionAsync();

        public async Task CloseConnectionAsync(DbConnection connection)
        {
            try
            {
                await connection.CloseAsync();
                await connection.DisposeAsync();
            }
            catch (Exception error)
            {
                throw new Exception($"Cannot close database connection: Message: ${error.Message}, StackTrace: {error.StackTrace}");
            }
        }

        public async Task<DbTransaction> BeginTransaction(DbConnection connection)
        {
            try
            {
                var transaction = await connection.BeginTransactionAsync();
                if (transaction != null)
                {
                    IsTransactionActive = true;
                    return transaction;
                }
                else
                {
                    IsTransactionActive = false;
                    throw new Exception($"Cannot begin transaction.");
                }
            }
            catch (Exception error)
            {
                throw new Exception($"Cannot begin transaction: Message: ${error.Message}, StackTrace: {error.StackTrace}");
            }
        }

        public async Task CloseTransaction(DbTransaction transaction, bool commit = true)
        {
            try
            {
                if (commit)
                {
                    await transaction.CommitAsync();
                    await transaction.DisposeAsync();
                }
                else
                {
                    await transaction.RollbackAsync();
                    await transaction.DisposeAsync();
                }

                IsTransactionActive = false;
            }
            catch (Exception error)
            {
                throw new Exception($"Cannot close transaction: Message: ${error.Message}, StackTrace: {error.StackTrace}");
            }
        }

        public async Task RollbackTransaction(DbTransaction transaction, string? savePointName = null)
        {
            try
            {
                if (savePointName != null)
                    await transaction.RollbackAsync(savePointName);
                else
                {
                    await transaction.RollbackAsync();
                }

            }
            catch (Exception error)
            {
                throw new Exception($"Cannot rollback transaction: Message: ${error.Message}, StackTrace: {error.StackTrace}");
            }
        }

        public async Task SaveTransaction(DbTransaction transaction, string savePointName)
        {
            try
            {
                await transaction.SaveAsync(savePointName);
            }
            catch (Exception error)
            {
                throw new Exception($"Cannot save transaction: Message: ${error.Message}, StackTrace: {error.StackTrace}");
            }
        }

        public async Task ReleaseSavePoint(DbTransaction transaction, string savePointName)
        {
            try
            {
                await transaction.ReleaseAsync(savePointName);
            }
            catch (Exception error)
            {
                throw new Exception($"Cannot release transaction save point: Message: ${error.Message}, StackTrace: {error.StackTrace}");
            }
        }
    }
}
