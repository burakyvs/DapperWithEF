using System.Data.Common;

namespace DataAccess.Abstract
{
    public interface IDbConnector
    {
        public string ConnectionString { get; set; }
        public bool IsTransactionActive { get; set; }
        public Task<DbConnection> OpenConnectionAsync();
        public Task CloseConnectionAsync(DbConnection connection);
        public Task<DbTransaction> BeginTransaction(DbConnection connection);
        public Task CloseTransaction(DbTransaction transaction, bool commit = true);
        public Task RollbackTransaction(DbTransaction transaction, string? savePointName = null);
        public Task SaveTransaction(DbTransaction transaction, string savePointName);
        public Task ReleaseSavePoint(DbTransaction transaction, string savePointName);
    }
}
