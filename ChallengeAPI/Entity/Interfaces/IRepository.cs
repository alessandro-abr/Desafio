using System;
using System.Linq;
using System.Linq.Expressions;

namespace ChallengeAPI.DataAccess.Interfaces
{
    public interface IRepository<T> 
    {
       
        IContext db { get; set; }
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        T FindSingle(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includes);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includes);
        IQueryable<T> FindIncluding(params Expression<Func<T, object>>[] includeProperties);
        int Count(Expression<Func<T, bool>> predicate = null);
        bool Exist(Expression<Func<T, bool>> predicate = null);
        int SaveChanges();
        bool Validate { set; }
    }
}
