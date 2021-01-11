using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Synergy.ReliefCenter.Data.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        List<TEntity> Get();

        IEnumerable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression);

        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void Save();

        TEntity Get(long id);

        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);
    }
}
