using DataAccess.Concrete.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityFramework.Contexts
{
    public class SqlServerDbContext : ProjectDbContext
    {
        private static Type DbConnectorType = typeof(SqlServerDbConnector);
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options)
        { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseSqlServer(ConnectionString));
            }
        }
    }
}
