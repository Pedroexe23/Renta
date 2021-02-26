namespace RentaWEB2._0.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Funcionario")]
    public partial class Funcionario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Id_Funcionario { get; set; }

        public short Activo { get; set; }

        [StringLength(10)]
        public string Rut { get; set; }

        [StringLength(40)]
        public string Apellidos { get; set; }

        [StringLength(40)]
        public string Nombres { get; set; }

        public short Sexo { get; set; }

        public short EstadoCivil { get; set; }

        public DateTime? Fec_nacimiento { get; set; }

        [StringLength(40)]
        public string Direccion { get; set; }

        public int RentaPromedio { get; set; }

        public virtual Causante Causante { get; set; }
    }
}
