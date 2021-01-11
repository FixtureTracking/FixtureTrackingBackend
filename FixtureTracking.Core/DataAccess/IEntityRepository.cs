using FixtureTracking.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FixtureTracking.Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        T Get(Expression<Func<T, bool>> filter);
        IList<T> GetList(Expression<Func<T, bool>> fiter = null);
        bool Any(Expression<Func<T, bool>> filter);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
