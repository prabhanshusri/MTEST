using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MTest.Services
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);

        void Create(T entity);
        void Update(T entity);

        IQueryable<T> Get(params Expression<Func<T, object>>[] navigationProperties);
    }
}
