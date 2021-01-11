using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Repositories;

namespace Synergy.CrewWage.Data.Repositories.SeafarerRepository
{
    public class BaseSeafarerRepository<T> : IBaseRepository<T> where T : class
    {
        protected SeafarerContext SeafarerContext;

        protected BaseSeafarerRepository(SeafarerContext seafarerContext)
        {
            SeafarerContext = seafarerContext;
        }

        public List<T> Get()
        {
            return SeafarerContext.Set<T>().ToList();
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return SeafarerContext.Set<T>().Where(expression);
        }

        public void Insert(T entity)
        {
            SeafarerContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            SeafarerContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            SeafarerContext.Set<T>().Remove(entity);
        }

        public void Save()
        {
            SeafarerContext.SaveChanges();
        }

        public T Get(long id)
        {
            return SeafarerContext.Set<T>().Find(id);
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] propertySelectors)
        {
            var set = propertySelectors
                .Aggregate<Expression<Func<T, object>>, IQueryable<T>>
                    (SeafarerContext.Set<T>(), (current, expression) =>
                    {
                        return current.Include(expression);
                    });

            return set;
        }
    }
}
