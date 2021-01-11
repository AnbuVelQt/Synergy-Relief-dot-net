using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Repositories;

namespace Synergy.CrewWage.Data.Repositories.ManningRepository
{
    public class BaseManningRepository<T> : IBaseRepository<T> where T : class
    {
        protected ManningContext ManningContext;

        protected BaseManningRepository(ManningContext manningContext)
        {
            ManningContext = manningContext;
        }

        public List<T> Get()
        {
            return ManningContext.Set<T>().ToList();
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return ManningContext.Set<T>().Where(expression);
        }

        public void Insert(T entity)
        {
            ManningContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            ManningContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            ManningContext.Set<T>().Remove(entity);
        }

        public void Save()
        {
            ManningContext.SaveChanges();
        }

        public T Get(long id)
        {
            return ManningContext.Set<T>().Find(id);
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] propertySelectors)
        {
            var set = propertySelectors
                .Aggregate<Expression<Func<T, object>>, IQueryable<T>>
                    (ManningContext.Set<T>(), (current, expression) =>
                    {
                        return current.Include(expression);
                    });

            return set;
        }
    }
}
