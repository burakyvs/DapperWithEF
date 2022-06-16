using Entity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataAccess.EntityFramework.Contexts
{
    public abstract class DbContextBase : DbContext
    {
        public static string ConnectionString { get; set; } = string.Empty;

        public DbSet<Product> Products { get; set; }
        

        public DbContextBase(DbContextOptions options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p => p.Id);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
