namespace RentaWEB2._0.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Documento")]
    public partial class Documento
    {
        
        [Key]
        public int Id_documento { get; set; }

        [StringLength(60)]
        public string Archivo { get; set; }

        public long Tamaño { get; set; }

        [StringLength(10)]
        public string tipo { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }
    }
}
