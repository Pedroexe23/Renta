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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short NUM_CORRELATIVO { get; set; }

        [StringLength(50)]
        public string RUT_CAUSANTE { get; set; }

        [StringLength(50)]
        public string NOMBRE_CAUSANTE { get; set; }

        public int CODIGO_TIPO_CAUSANTE { get; set; }

        [StringLength(100)]
        public string TIPO_CAUSANTE { get; set; }

        [StringLength(50)]
        public string RUT_BENEFICIARIO { get; set; }

        [StringLength(50)]
        public string NOMBRE_BENEFICIARIO { get; set; }

        public int CODIGO_TIPO_BENEFICIARIO { get; set; }

        [StringLength(50)]
        public string TIPO_BENEFICIARIO { get; set; }

        public int CODIGO_TIPO_BENEFICIO { get; set; }

        [StringLength(50)]
        public string TIPO_BENEFICIO { get; set; }

        [StringLength(50)]
        public string RUT_EMPLEADOR { get; set; }

        [StringLength(50)]
        public string NOMBRE_EMPLEADOR { get; set; }

        [Column(TypeName = "date")]
        public DateTime FECHA_RECONOCIMIENTO { get; set; }

        public short TRAMO { get; set; }

        public int MONTO_BENEFICIO { get; set; }

        public int CODIGO_ESTADO_TUPLA { get; set; }

        [StringLength(50)]
        public string GLOSA_ESTADO_TUPLA { get; set; }

        public int PROMEDIO_RENTA { get; set; }

        public virtual Asignacion_Familiar Asignacion_Familiar { get; set; }

        public virtual Funcionario Funcionario { get; set; }
    }
}
