using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentaWEB2._0.Models
{
    public class Archivos 
    {
        public IEnumerable<HttpPostedFileBase> files { get; set; }
        public string Doc { get; set; }
        public long tamaño { get; set; }
        public string tipo { get; set; }
        public DateTime Fecha { get; set; }
    }
}