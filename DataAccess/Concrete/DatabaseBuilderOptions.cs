using DataAccess.Abstract;
using DataAccess.Concrete.Dapper;
using DataAccess.EntityFramework.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Concrete
{
    public class DatabaseBuilderOptions : IDatabaseBuilderOptions
    {
        private readonly IServiceCollection _services;
        protected IConfiguration Configuration { get; }
        public DatabaseBuilderOptions(IServiceCollection services)
        {
            _services = services;
        }
        public void AddDataAccessor()
        {
            _services.AddTransient(typeof(IDataAccessor<>), typeof(DapperAccessor<>));
        }
    }
}
