using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Synergy.CrewWage.Data.Repositories;
using Synergy.ReliefCenter.Data.Contexts;

namespace Synergy.ReliefCenter.Data.Repositories.ReliefRepository
{
    public class BaseReliefRepository<T> : IBaseRepository<T> where T : class
    {
        protected ReliefContext ReliefContext;

        protected BaseReliefRepository(ReliefContext reliefContext)
        {
            ReliefContext = reliefContext;
        }

        public List<T> Get()
        {
            return ReliefContext.Set<T>().ToList();
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return ReliefContext.Set<T>().Where(expression);
        }

        public void Insert(T entity)
        {
            ReliefContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            ReliefContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            ReliefContext.Set<T>().Remove(entity);
        }

        public void Save()
        {
            ReliefContext.SaveChanges();
        }

        public T Get(long id)
        {
            return ReliefContext.Set<T>().Find(id);
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] propertySelectors)
        {
            var set = propertySelectors
                .Aggregate<Expression<Func<T, object>>, IQueryable<T>>
                    (ReliefContext.Set<T>(), (current, expression) =>
                    {
                        return current.Include(expression);
                    });

            return set;
        }
    }
}
