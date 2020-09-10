using System;
using System.Data.Entity;

namespace ChallengeAPI.DataAccess.Interfaces
{
    public interface IContext : IDisposable
    {
        IDbSet<T> GetEntitySet<T>() where T : class;
        void ChangeState<T>(T entity, EntityState state) where T : class;
        int SaveChanges();       
    }
}
