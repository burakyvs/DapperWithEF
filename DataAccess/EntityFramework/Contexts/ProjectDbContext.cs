using DataAccess.Concrete.PostgreSql;
using Entity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataAccess.EntityFramework.Contexts
{
    public class ProjectDbContext : DbContext
    {
        public static string ConnectionString { get; set; }
        private static Type DbConnectorType = typeof(PostgreSqlDbConnector);
        public DbSet<Product> Products { get; set; }
        

        public ProjectDbContext(DbContextOptions options)
            : base(options)
        {
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p => p.Id);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseNpgsql(ConnectionString));
            }
        }
    }
}
