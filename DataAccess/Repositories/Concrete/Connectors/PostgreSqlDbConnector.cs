using DataAccess.Abstract;
using Npgsql;
using System.Data.Common;

namespace DataAccess.Repositories.Concrete.Connectors
{
    public class PostgreSqlDbConnector : DbConnectorBase
    {
        public override async Task<DbConnection> OpenConnectionAsync()
        {
            try
            {
                DbConnection connection = new NpgsqlConnection(ConnectionString);
                await connection.OpenAsync();

                return connection;
            }
            catch (Exception error)
            {
                throw new Exception($"Cannot open database connection: Message: ${error.Message}, StackTrace: {error.StackTrace}");
            }
        }
    }
}
