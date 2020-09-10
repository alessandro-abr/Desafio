using ChallengeAPI.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeAPI.Entity
{
    public partial class Context : DbContext
    {
        public DbSet<Directory> Directorys { get; set; }
        public DbSet<Geometry> Geometries { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this.Configuration.LazyLoadingEnabled = false;
            modelBuilder.Configurations.Add(new DirectoryMap());
            modelBuilder.Configurations.Add(new GeometryMap());
        }
    }
}
