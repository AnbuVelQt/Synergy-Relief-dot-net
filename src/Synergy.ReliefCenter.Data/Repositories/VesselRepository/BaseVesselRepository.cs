using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Synergy.ReliefCenter.Data.Contexts;

namespace Synergy.ReliefCenter.Data.Repositories.VesselRepository
{
    public class BaseVesselRepository<T> : IBaseRepository<T> where T : class
    {
        protected VesselContext VesselContext;

        protected BaseVesselRepository(VesselContext vesselContext)
        {
            VesselContext = vesselContext;
        }

        public List<T> Get()
        {
            return VesselContext.Set<T>().ToList();
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return VesselContext.Set<T>().Where(expression);
        }

        public void Insert(T entity)
        {
            VesselContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            VesselContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            VesselContext.Set<T>().Remove(entity);
        }

        public void Save()
        {
            VesselContext.SaveChanges();
        }

        public T Get(long id)
        {
            return VesselContext.Set<T>().Find(id);
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] propertySelectors)
        {
            var set = propertySelectors
                .Aggregate<Expression<Func<T, object>>, IQueryable<T>>
                    (VesselContext.Set<T>(), (current, expression) =>
                    {
                        return current.Include(expression);
                    });

            return set;
        }
    }
}
