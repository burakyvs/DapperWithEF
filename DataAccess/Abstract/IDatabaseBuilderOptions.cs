using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Abstract
{
    public interface IDatabaseBuilderOptions
    {
        public void AddDataAccessor();
    }
}
