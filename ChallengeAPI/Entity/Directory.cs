using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ChallengeAPI.Entity;

namespace ChallengeAPI.Entity
{
    public class Directory
    {
        public Directory()
        {
            this.Geometric = new List<Geometry>();
        }
        public Guid id { get; set; }
        
        [Required(ErrorMessage ="Por favor, informe um nome")]
        [StringLength(100, ErrorMessage = "Nome do diretório deve ter no mínimo {2} e no maximo {1} caracteres", MinimumLength = 5)]
        public string nmDirectory { get; set; }

        public ICollection<Geometry> Geometric { get; set; }
    }
}
namespace ChallengeAPI.Mapping
{
    public class DirectoryMap : EntityTypeConfiguration<Directory>
    {
        public DirectoryMap()
        {
            // Primary Key
            this.HasKey(t => t.id)
                .Property(t => t.id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.nmDirectory)
               .IsRequired()
               .HasMaxLength(100);
                       

            // Table & Column Mappings
            this.ToTable("tbdirectory", Context.Schema);           
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.nmDirectory).HasColumnName("nmdirectory");  
        }

    }
}
