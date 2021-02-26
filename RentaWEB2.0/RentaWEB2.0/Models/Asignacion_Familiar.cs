namespace RentaWEB2._0.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Asignacion_Familiar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Asignacion_Familiar()
        {
            Causante = new HashSet<Causante>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Tramo { get; set; }

        public int Monto { get; set; }

        [StringLength(50)]
        public string Requisito_De_Sistema { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Causante> Causante { get; set; }
    }
}
