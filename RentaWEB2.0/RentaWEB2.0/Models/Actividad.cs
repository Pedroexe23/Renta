using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentaWEB2._0.Models
{
    public class Actividad
    {
        public short NUM_CORRELATIVO { get; set; }
        public string RUT_CAUSANTE { get; set; }
        public string NOMBRE_CAUSANTE { get; set; }
        public int CODIGO_TIPO_CAUSANTE { get; set; }
        public string TIPO_CAUSANTE { get; set; }
        public string RUT_BENEFICIARIO { get; set; }
        public string NOMBRE_BENEFICIARIO { get; set; }
        public int CODIGO_TIPO_BENEFICIARIO { get; set; }
        public string TIPO_BENEFICIARIO { get; set; }
        public int CODIGO_TIPO_BENEFICIO { get; set; }
        public string TIPO_BENEFICIO { get; set; }
        public string RUT_EMPLEADOR { get; set; }
        public string NOMBRE_EMPLEADOR { get; set; }
        public DateTime FECHA_RECONOCIMIENTO { get; set; }
        public short TRAMO { get; set; }
        public int MONTO_BENEFICIO { get; set; }
        public int CODIGO_ESTADO_TUPLA { get; set; }
        public string GLOSA_ESTADO_TUPLA { get; set; }
        public int PROMEDIO_RENTA { get; set; }
        public short ACTIVO { get; set; }
    }
}
