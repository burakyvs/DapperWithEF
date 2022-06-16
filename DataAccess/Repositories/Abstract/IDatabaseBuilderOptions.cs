using Microsoft.Extensions.Configuration;

namespace DataAccess.Abstract
{
    public interface IDatabaseBuilderOptions
    {
        public IConfiguration Configuration { get; set; }
        public void AddDataAccessor();
    }
}
