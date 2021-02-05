namespace RentaWEB2._0.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Asignacion_Familiar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Tramo { get; set; }

        public int? Monto { get; set; }

        [StringLength(50)]
        public string Requisito_De_Sistema { get; set; }
    }
}
