using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Concrete.Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection InitDatabaseConnection<TDbContext>(this IServiceCollection services, Action<IDatabaseBuilderOptions> databaseBuilderOptions) where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>();

            IDatabaseBuilderOptions databaseBuilder = new DatabaseBuilderOptions(services);

            databaseBuilderOptions.Invoke(databaseBuilder);

            return services;
        }
    }
}
