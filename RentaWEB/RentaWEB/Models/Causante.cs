namespace RentaWEB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Causante")]
    public partial class Causante
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Num_Correlativo { get; set; }

        [StringLength(10)]
        public string Rut_Causante { get; set; }

        [StringLength(50)]
        public string Nombre_Causante { get; set; }

        public int? Cod_Tipo_Causante { get; set; }

        [StringLength(100)]
        public string Tipo_Causante { get; set; }

        [StringLength(10)]
        public string Rut_beneficiario { get; set; }

        [StringLength(50)]
        public string Nombre_beneficiario { get; set; }

        public int? Codigo_Tipo_beneficiario { get; set; }

        [StringLength(25)]
        public string Tipo_beneficiario { get; set; }

        public int? Codigo_Tipo_Beneficio { get; set; }

        [StringLength(25)]
        public string Tipo_Beneficio { get; set; }

        [StringLength(10)]
        public string Rut_Empleador { get; set; }

        [StringLength(50)]
        public string Nombre_Empleador { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Fecha_Reconocimiento { get; set; }

        public int? Tramo { get; set; }

        public int? Monto_Beneficiario { get; set; }

        public int? Codigo_Estado_Tupla { get; set; }

        [StringLength(30)]
        public string Glosa_Estado_Tupla { get; set; }

        public int? Renta_Promedio { get; set; }
    }
}
