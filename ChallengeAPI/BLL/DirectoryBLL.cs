using ChallengeAPI.DataAccess.Interfaces;
using ChallengeAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeAPI.BLL
{
    public class DirectoryBLL : ChallengeAPI.DataAccess.Repository<Entity.Directory>
    {
        public DirectoryBLL(IContext db) : base(db){ }

        protected override void BeforeInsert(Directory bean)
        {
            bean.id = Guid.NewGuid();

        }
    }
}
