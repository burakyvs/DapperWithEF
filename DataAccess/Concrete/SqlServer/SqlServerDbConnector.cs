using DataAccess.Abstract;
using System.Data.Common;
using System.Data.SqlClient;

namespace DataAccess.Concrete.SqlServer
{
    public class SqlServerDbConnector : DbConnectorBase
    {
        public override async Task<DbConnection> OpenConnectionAsync()
        {
            try
            {
                DbConnection connection = new SqlConnection(ConnectionString);
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
