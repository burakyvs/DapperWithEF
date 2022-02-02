using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Concrete.Dapper;
using DataAccess.Concrete.PostgreSql;
using DataAccess.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection InitDatabaseConnection<TDbContext>(this IServiceCollection services, Action<IDatabaseBuilderOptions> databaseBuilderOptions) where TDbContext : DbContext
        {
            IDatabaseBuilderOptions databaseBuilder = new DatabaseBuilderOptions(services);

            databaseBuilderOptions.Invoke(databaseBuilder);

            typeof(TDbContext).SetConnectionString(databaseBuilder.Configuration);

            services.AddDbContext<TDbContext>();

            var dbConnectorType = typeof(TDbContext).GetDbConnectorType();

            services.AddDbConnector(dbConnectorType);

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
