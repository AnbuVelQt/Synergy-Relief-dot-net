using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Models;

namespace Synergy.ReliefCenter.Data.Repositories.ReliefRepository
{
    public class BaseReliefRepository<T> : IBaseRepository<T> where T : class
    {
        protected synergy_manningContext ReliefContext;

        protected BaseReliefRepository(synergy_manningContext reliefContext)
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

        
        public async Task<T> InsertAsync(T entity)
        {
            await ReliefContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Update(T entity)
        {
            ReliefContext.Set<T>().Update(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            ReliefContext.Set<T>().UpdateRange(entity);
            await SaveAsync();
        }

        public void Delete(T entity)
        {
            ReliefContext.Set<T>().Remove(entity);
        }

        public virtual int Save()
        {
            return ReliefContext.SaveChanges();
        }
        public async Task<int> SaveAsync()
        {
            return await ReliefContext.SaveChangesAsync();
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
