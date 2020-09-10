using ChallengeAPI.DataAccess.Interfaces;
using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace ChallengeAPI.DataAccess
{
    //[Export(typeof(IRepository<>))]
    public class Repository<T> : IRepository<T> where T : class
    {
        private IContext _db;
       
        private bool _validate = true;

        protected virtual void BeforeInsert(T bean) { }
        protected virtual void AfterInsert(T bean) { }
        protected virtual void BeforeUpdate(T bean) { }
        protected virtual void AfterUpdate(T bean) { }
        protected virtual void BeforeDelete(T bean) { }
        protected virtual void AfterDelete(T bean) { }

        protected virtual void ValidInsert(T bean) { }
        protected virtual void ValidUpdate(T bean) { }
        protected virtual void ValidDelete(T bean) { }

        protected virtual void CheckForeignKey(T bean) { }

        [Obsolete("Constructor is deprecated, please use Constructor(db) instead.")]
        public Repository(IContext db)
        {
            _db = db;         
        }       

        public IContext db { get { return _db; } set { _db = value; } }    

        public virtual void Insert(T entity)
        {           
            CheckForeignKey(entity);
            if(_validate)
                ValidInsert(entity);
            BeforeInsert(entity);
            this.db.GetEntitySet<T>().Add(entity);
            AfterInsert(entity);
        }
        public virtual void Update(T entity)
        {           
            CheckForeignKey(entity);
            if (_validate)
                ValidUpdate(entity);
            BeforeUpdate(entity);
            this.db.ChangeState(entity, System.Data.Entity.EntityState.Modified);
            AfterUpdate(entity);
        }
        public T FindSingle(System.Linq.Expressions.Expression<Func<T, bool>> predicate = null, params System.Linq.Expressions.Expression<Func<T, object>>[] includes)
        {
            var set = FindIncluding(includes);
            return (predicate == null) ?
                   set.FirstOrDefault() :
                   set.FirstOrDefault(predicate);
        }
        public IQueryable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate = null, params System.Linq.Expressions.Expression<Func<T, object>>[] includes)
        {
            var set = FindIncluding(includes);
            return (predicate == null) ? set : set.Where(predicate);
        }
        public IQueryable<T> FindIncluding(params System.Linq.Expressions.Expression<Func<T, object>>[] includeProperties)
        {
            var set = this.db.GetEntitySet<T>();
            IQueryable<T> query = set.AsQueryable();

            if (includeProperties != null)
            {
                foreach (var include in includeProperties)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }
        public int Count(System.Linq.Expressions.Expression<Func<T, bool>> predicate = null)
        {
            var set = this.db.GetEntitySet<T>();
            return (predicate == null) ?
                   set.Count() :
                   set.Count(predicate);
        }
        public bool Exist(System.Linq.Expressions.Expression<Func<T, bool>> predicate = null)
        {
            var set = this.db.GetEntitySet<T>();
            return (predicate == null) ? set.Any() : set.Any(predicate);
        }
        public virtual void Delete(T entity)
        {
            if (_validate)
                ValidDelete(entity);
            BeforeDelete(entity);
            this.db.ChangeState(entity, System.Data.Entity.EntityState.Deleted);
            AfterDelete(entity);
        }
        public virtual void Delete(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            var entity = this.Find(predicate).ToList();
            entity.ForEach(u => this.Delete(u));
        }
        public int SaveChanges()
        {
            return _db.SaveChanges();
        }
        public void ExecStartemet(string stm, string connString)
        {
            var conn = new SqlConnection(connString);
            conn.Open();

            var comm = new SqlCommand(stm, conn);
            comm.CommandTimeout = 9000;

            comm.ExecuteNonQuery();
            conn.Close();
        }
        public bool Validate { set { _validate = value; } }

        public void AddToContext(T entity)
        {
            this.db.GetEntitySet<T>().Add(entity);
        }
    }
}
