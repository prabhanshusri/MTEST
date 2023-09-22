using Microsoft.EntityFrameworkCore;
using MTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MTest.Services
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected MTDBContext _dbContext { get; set; }
        public RepositoryBase(MTDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<T> FindAll() => _dbContext.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
        _dbContext.Set<T>().Where(expression).AsNoTracking();

        public void Create(T entity) => _dbContext.Set<T>().Add(entity);
        public void Update(T entity) => _dbContext.Set<T>().Update(entity);


        public IQueryable<T> Get(params Expression<Func<T, object>>[] navigationProperties)
        {
            if (navigationProperties != null)
            {
                IQueryable<T> dbQuery = _dbContext.Set<T>();
                foreach (var np in navigationProperties)
                {
                    dbQuery = dbQuery.Include<T, object>(np);
                }

                return dbQuery;
            }
            return null;
        }
    }
}
