using Synergy.ReliefCenter.Data.Repositories.Abstraction.PolicyRepository;
using Microsoft.EntityFrameworkCore;
using Synergy.ReliefCenter.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Data.Repositories
{
   
    public class BasePoliciesRepository<T> : IBasePoliciesRepository<T> where T : class
    {
        protected MasterDbContext MasterDbContext;

        protected BasePoliciesRepository(MasterDbContext masterDbContext)
        {
            MasterDbContext = masterDbContext;
        }

        public List<T> Get()
        {
            return MasterDbContext.Set<T>().ToList();
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return MasterDbContext.Set<T>().Where(expression);
        }

        public void Insert(T entity)
        {
            MasterDbContext.Set<T>().Add(entity);
        }


        public async Task<T> InsertAsync(T entity)
        {
            await MasterDbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Update(T entity)
        {
            MasterDbContext.Set<T>().Update(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            MasterDbContext.Set<T>().UpdateRange(entity);
            await SaveAsync();
        }

        public void Delete(T entity)
        {
            MasterDbContext.Set<T>().Remove(entity);
        }

        public virtual int Save()
        {
            return MasterDbContext.SaveChanges();
        }
        public async Task<int> SaveAsync()
        {
            return await MasterDbContext.SaveChangesAsync();
        }
        public T Get(long id)
        {
            return MasterDbContext.Set<T>().Find(id);
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] propertySelectors)
        {
            var set = propertySelectors
                .Aggregate<Expression<Func<T, object>>, IQueryable<T>>
                    (MasterDbContext.Set<T>(), (current, expression) =>
                    {
                        return current.Include(expression);
                    });
            return set;
        }
    }
}
