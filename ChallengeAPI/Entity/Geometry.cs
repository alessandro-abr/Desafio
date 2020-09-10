using ChallengeAPI.Entity;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;


namespace ChallengeAPI.Entity
{
    public class Geometry
    {
        public enum Type
        {
            QUADRADO = 0,
            TRIANGULO = 1
        }
        public Guid id { get; set; }

        [Required(ErrorMessage = "Por favor, informe um diretório")]

        public Guid idDirectory { get; set; }

        [Required(ErrorMessage = "Por favor, informe um nome")]
        [StringLength(100, ErrorMessage = "Nome do objeto deve ter no mínimo {2} e no maximo {1} caracteres", MinimumLength = 5)]
        public string nmGeometry { get; set; }

        [Required(ErrorMessage = "Por favor, informe um tipo")]
        [EnumDataType(typeof(Type), ErrorMessage = "Tipo da forma geométrica Inválida ")]
        public byte nuType { get; set; }

        [Required(ErrorMessage = "Por favor, informe uma cor")]
        [StringLength(100, ErrorMessage = "Cor do objeto deve ter no mínimo {2} e no maximo {1} caracteres", MinimumLength = 3)]
        public string deColor { get; set; }

        [Required(ErrorMessage = "Por favor, informe um tamanho")]
        public int nuPixel { get; set; }
        
        public Directory Directory { get; set; }
    }
}
namespace ChallengeAPI.Mapping
{
    public class GeometryMap : EntityTypeConfiguration<Geometry>
    {
        public GeometryMap()
        {
            // Primary Key
            this.HasKey(t => t.id)
                .Property(t => t.id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.nmGeometry)
               .IsRequired()
               .HasMaxLength(100);

            this.Property(t => t.deColor)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("tbgeometry", Context.Schema);
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.idDirectory).HasColumnName("iddirectory");
            this.Property(t => t.nmGeometry).HasColumnName("nmgeometry");
            this.Property(t => t.nuType).HasColumnName("nutype");
            this.Property(t => t.deColor).HasColumnName("decolor");
            this.Property(t => t.nuPixel).HasColumnName("nupixel");

            // Relationships
            this.HasRequired(t => t.Directory)
                .WithMany(t => t.Geometric)
                .HasForeignKey(d => d.idDirectory);
        }

    }
}

