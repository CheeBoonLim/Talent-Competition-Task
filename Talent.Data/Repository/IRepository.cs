using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Talent.Data
{
    public interface IRepository<T> where T : class
    {
        T Create();
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        T GetById(object id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> Table { get; }
        IQueryable<T> GetQueryable(bool includeDeleted = false);
    }
}
