using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework.Contexts
{
    public class SqlServerDbContext : ProjectDbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options, IConfiguration configuration) : base(options, configuration)
        {
            ConnectionString = Configuration.GetConnectionString("SqlServerConnection");
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
