namespace RentaWEB2._0.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Documento")]
    public partial class Documento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_Documento { get; set; }

        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(100)]
        public string NombreReal { get; set; }

        [Column("documento")]
        public byte[] documento1 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Fecha { get; set; }
    }
}
