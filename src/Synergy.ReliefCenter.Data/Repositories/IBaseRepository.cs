using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Data.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        List<TEntity> Get();

        IEnumerable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression);

        void Insert(TEntity entity);

        Task<TEntity> InsertAsync(TEntity entity);

        void Update(TEntity entity);

        Task UpdateAsync(TEntity entity);
        void Delete(TEntity entity);

        int Save();

        Task<int> SaveAsync();

        TEntity Get(long id);

        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);
    }
}
