using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ChallengeAPI.Entity
{
    public partial class Context : DbContext, DataAccess.Interfaces.IContext
    {
        public Context() : base(CC("Entities.ChallengeAPI"))
        {

            Database.SetInitializer<Context>(new CreateDatabaseIfNotExists<Context>());

            this.Configuration.ValidateOnSaveEnabled = false;

            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 180;
        }
    }
}
