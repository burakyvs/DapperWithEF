using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IDataAccessor<TEntity>
    {
        public void AddAsync(TEntity entity);
        public void DeleteAsync(TEntity entity);
        public void AddRangeAsync(ICollection<TEntity> entities);
        public void DeleteRangeAsync(ICollection<TEntity> entities);
        public void UpdateAsync(TEntity entity);
        public void GetByIdAsync(int id);
        public void GetAllAsync(Expression expression);
    }
}
