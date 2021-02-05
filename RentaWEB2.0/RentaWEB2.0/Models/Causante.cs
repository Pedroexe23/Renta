namespace RentaWEB2._0.Models
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
        [Column(Order = 0)]
        public byte NUM_CORRELATIVO { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string RUT_CAUSANTE { get; set; }

        [StringLength(50)]
        public string NOMBRE_CAUSANTE { get; set; }

        [Key]
        [Column(Order = 2)]
        public byte CODIGO_TIPO_CAUSANTE { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string TIPO_CAUSANTE { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(50)]
        public string RUT_BENEFICIARIO { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(50)]
        public string NOMBRE_BENEFICIARIO { get; set; }

        [Key]
        [Column(Order = 6)]
        public byte CODIGO_TIPO_BENEFICIARIO { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(50)]
        public string TIPO_BENEFICIARIO { get; set; }

        [Key]
        [Column(Order = 8)]
        public byte CODIGO_TIPO_BENEFICIO { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(50)]
        public string TIPO_BENEFICIO { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(50)]
        public string RUT_EMPLEADOR { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(50)]
        public string NOMBRE_EMPLEADOR { get; set; }

        [Key]
        [Column(Order = 12, TypeName = "date")]
        public DateTime FECHA_RECONOCIMIENTO { get; set; }

        [Key]
        [Column(Order = 13)]
        public byte TRAMO { get; set; }

        [Key]
        [Column(Order = 14)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short MONTO_BENEFICIO { get; set; }

        [Key]
        [Column(Order = 15)]
        public byte CODIGO_ESTADO_TUPLA { get; set; }

        [Key]
        [Column(Order = 16)]
        [StringLength(50)]
        public string GLOSA_ESTADO_TUPLA { get; set; }

        public int? PROMEDIO_RENTA { get; set; }
    }
}
