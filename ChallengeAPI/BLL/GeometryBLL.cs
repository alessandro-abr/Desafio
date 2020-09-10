using ChallengeAPI.DataAccess.Interfaces;
using ChallengeAPI.Entity;
using System;

namespace ChallengeAPI.BLL
{
    public class GeometryBLL : ChallengeAPI.DataAccess.Repository<Geometry>
    {
        public GeometryBLL(IContext db) : base(db){ }

        protected override void BeforeInsert(Geometry bean)
        {
            bean.id = Guid.NewGuid();
        }
    }
}
