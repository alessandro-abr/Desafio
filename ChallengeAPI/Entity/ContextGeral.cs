
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ChallengeAPI.Entity
{
    public partial class Context : DbContext
    {     
        public static string Schema { get { return System.Configuration.ConfigurationManager.AppSettings["schema"]; } }
        public static string CC(string name)
        {
            var conn = "Data Source=RP-PC01; Initial Catalog=ChallengeDB; User ID=desenvteste; Password={0}";
            var pass = "master123";
            return string.Format(conn, pass);
        }        

        #region IContext Implementation

        public void ChangeState<T>(T entity, EntityState state) where T : class
        {
            Entry<T>(entity).State = state;
        }

        public IDbSet<T> GetEntitySet<T>()
        where T : class
        {
            return Set<T>();
        }           

        #endregion
    }
}
