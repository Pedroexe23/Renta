namespace RentaWEB.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CausanteModel : DbContext
    {
        public CausanteModel()
            : base("name=CausanteModel")
        {
        }

        public virtual DbSet<Causante> Causante { get; set; }
        public virtual DbSet<Funcionario> Funcionario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Causante>()
                .Property(e => e.Rut_Causante)
                .IsUnicode(false);

            modelBuilder.Entity<Causante>()
                .Property(e => e.Nombre_Causante)
                .IsUnicode(false);

            modelBuilder.Entity<Causante>()
                .Property(e => e.Tipo_Causante)
                .IsUnicode(false);

            modelBuilder.Entity<Causante>()
                .Property(e => e.Rut_beneficiario)
                .IsUnicode(false);

            modelBuilder.Entity<Causante>()
                .Property(e => e.Nombre_beneficiario)
                .IsUnicode(false);

            modelBuilder.Entity<Causante>()
                .Property(e => e.Tipo_beneficiario)
                .IsUnicode(false);

            modelBuilder.Entity<Causante>()
                .Property(e => e.Tipo_Beneficio)
                .IsUnicode(false);

            modelBuilder.Entity<Causante>()
                .Property(e => e.Rut_Empleador)
                .IsUnicode(false);

            modelBuilder.Entity<Causante>()
                .Property(e => e.Nombre_Empleador)
                .IsUnicode(false);

            modelBuilder.Entity<Causante>()
                .Property(e => e.Glosa_Estado_Tupla)
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
