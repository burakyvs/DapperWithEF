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
        public static IServiceCollection InitDatabaseConnection<TDbContext>(this IServiceCollection services, Action<IDatabaseBuilderOptions> databaseBuilderOptions) where TDbContext : DbContext
        {
            if (_isDatabaseInitialized) throw new InvalidOperationException("A database connection already initialized.");

            IDatabaseBuilderOptions databaseBuilder = new DatabaseBuilderOptions(services);

            databaseBuilderOptions.Invoke(databaseBuilder);

            var configuration = databaseBuilder.Configuration ?? throw new ArgumentNullException(nameof(databaseBuilder.Configuration));

            typeof(TDbContext).SetConnectionString(configuration);

            services.AddDbContext<TDbContext>();

            var dbConnectorType = typeof(TDbContext).GetDbConnectorType();

            services.AddDbConnector(dbConnectorType);

            _isDatabaseInitialized = true;

            return services;
        }

        private static void AddDbConnector(this IServiceCollection services, Type dbConnectorType)
        {
            services.AddTransient(typeof(IDbConnector), dbConnectorType);
        }

        private static void SetConnectionString(this Type dbContextType, IConfiguration configuration)
        {
            ProjectDbContext.ConnectionString = configuration.GetConnectionString(dbContextType.Name);
        }
    }
}
