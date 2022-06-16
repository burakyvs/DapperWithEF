using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Extensions
{
    public static class ConfigurationExtensions
    {
        public static bool IsDatabaseInitialized { get => _isDatabaseInitialized; }
        private static bool _isDatabaseInitialized = false;
        public static IServiceCollection InitDatabaseConnection<TDbContext>(this IServiceCollection services, Action<IDatabaseBuilderOptions> databaseBuilderOptions) where TDbContext : DbContext, IDbContext
        {
            if (_isDatabaseInitialized) throw new InvalidOperationException("A database connection already initialized.");

            IDatabaseBuilderOptions databaseBuilder = new DatabaseBuilderOptions(services);
            databaseBuilderOptions.Invoke(databaseBuilder);

            var configuration = databaseBuilder.Configuration ?? throw new ArgumentNullException(nameof(databaseBuilder.Configuration));
            SetConnectionString(typeof(TDbContext).Name, configuration);

            services.AddDbContext<TDbContext>();
            var dbConnectorType = GetDbConnectorType<TDbContext>();
            services.AddDbConnector(dbConnectorType);

            _isDatabaseInitialized = true;

            return services;
        }

        private static void AddDbConnector(this IServiceCollection services, Type dbConnectorType)
        {
            services.AddTransient(typeof(IDbConnector), dbConnectorType);
        }

        private static void SetConnectionString(string dbContextName, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString(dbContextName);

            if (string.IsNullOrEmpty(connectionString) || string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            DbContextBase.ConnectionString = connectionString;
        }

        public static Type GetDbConnectorType<TDbContext>() where TDbContext : IDbContext
        {
            string dbConnectorName = typeof(TDbContext).Name.Replace("Context", "Connector");
            Type? dbConnectorType = Type.GetType($"DataAccess.Repositories.Concrete.Connectors.{dbConnectorName}, DataAccess");

            if (dbConnectorType != null)
                return dbConnectorType;
            else
                throw new Exception($"DbContext <{typeof(TDbContext).Name}> has no matching <{dbConnectorName}> database connector class.");
        }
    }
}
