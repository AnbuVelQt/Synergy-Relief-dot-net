using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Repositories;

namespace Synergy.CrewWage.Data.Repositories.MasterRepository
{
    public class BaseMasterRepository<T> : IBaseRepository<T> where T : class
    {
        protected MasterContext MasterContext;

        protected BaseMasterRepository(MasterContext masterContext)
        {
            MasterContext = masterContext;
        }

        public List<T> Get()
        {
            return MasterContext.Set<T>().ToList();
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return MasterContext.Set<T>().Where(expression);
        }

        public void Insert(T entity)
        {
            MasterContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            MasterContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            MasterContext.Set<T>().Remove(entity);
        }

        public void Save()
        {
            MasterContext.SaveChanges();
        }

        public T Get(long id)
        {
            return MasterContext.Set<T>().Find(id);
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] propertySelectors)
        {
            var set = propertySelectors
                .Aggregate<Expression<Func<T, object>>, IQueryable<T>>
                    (MasterContext.Set<T>(), (current, expression) =>
                    {
                        return current.Include(expression);
                    });

            return set;
        }
    }
}
