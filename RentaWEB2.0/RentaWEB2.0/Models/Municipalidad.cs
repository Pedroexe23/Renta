using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace RentaWEB2._0.Models
{
    public partial class Municipalidad : DbContext
    {
        public Municipalidad()
            : base("name=Municipalidad")
        {
        }

        public virtual DbSet<Asignacion_Familiar> Asignacion_Familiar { get; set; }
        public virtual DbSet<Causante> Causante { get; set; }
        public virtual DbSet<Funcionario> Funcionario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asignacion_Familiar>()
                .Property(e => e.Requisito_De_Sistema)
                .IsUnicode(false);

            modelBuilder.Entity<Funcionario>()
                .Property(e => e.Rut)
                .IsUnicode(false);

            modelBuilder.Entity<Funcionario>()
                .Property(e => e.Apellidos)
                .IsUnicode(false);

            modelBuilder.Entity<Funcionario>()
                .Property(e => e.Nombres)
                .IsUnicode(false);

            modelBuilder.Entity<Funcionario>()
                .Property(e => e.Direccion)
                .IsUnicode(false);
        }
    }
}
