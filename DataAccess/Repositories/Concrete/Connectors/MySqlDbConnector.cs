using DataAccess.Abstract;
using MySqlConnector;
using System.Data.Common;

namespace DataAccess.Repositories.Concrete.Connectors
{
    public class MySqlDbConnector : DbConnectorBase
    {
        public override async Task<DbConnection> OpenConnectionAsync()
        {
            try
            {
                DbConnection connection = new MySqlConnection(ConnectionString);
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
