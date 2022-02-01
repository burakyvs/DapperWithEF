using DataAccess.Abstract;
using System.Data.Common;
using System.Data.SqlClient;

namespace DataAccess.Dapper
{
    public class DapperBase : IORMBase
    {
        public string ConnectionString { get => _connectionString; set => _connectionString = value; }
        private string _connectionString;

        public bool IsTransactionActive { get => _isTransactionActive; set => _isTransactionActive = value; }
        private bool _isTransactionActive;

        public DapperBase(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<SqlConnection> OpenConnectionAsync()
        {
            try
            {
                SqlConnection connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                return connection;
            }
            catch(Exception error)
            {
                throw new Exception($"Cannot open database connection: Message: ${error.Message}, StackTrace: {error.StackTrace}");
            }
        }

        public async Task CloseConnectionAsync(SqlConnection connection)
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

        public async Task<DbTransaction> BeginTransaction(SqlConnection connection)
        {
            try
            {
                var transaction = await connection.BeginTransactionAsync();
                if (transaction != null)
                {
                    _isTransactionActive = true;
                    return transaction;
                }
                else
                {
                    _isTransactionActive = false;
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
                if(commit)
                    await transaction.CommitAsync();
                else
                {
                    await transaction.RollbackAsync();
                    await transaction.DisposeAsync();
                }

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
