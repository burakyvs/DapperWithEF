using DataAccess.Concrete.MySql;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityFramework.Contexts
{
    public class MySqlDbContext : ProjectDbContext
    {
        private static Type DbConnectorType = typeof(MySqlDbConnector);
        public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));
                base.OnConfiguring(optionsBuilder.UseMySql(ConnectionString, serverVersion));
            }
        }
    }
}
