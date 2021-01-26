using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentaWEB.Models
{
    public class Causante
    {
        public int Num_Correlativo { get; set; }
        public String Rut_Causante { get; set; }
        public int Cod_Tipo_Causante { get; set; }
        public String Tipo_Causante { get; set; }
        public String Rut_beneficiario { get; set; }
        public String Nombre_beneficiario { get; set; }
        public int Codigo_Tipo_beneficiario { get; set; }
        public String Tipo_beneficiario { get; set; }       
        public int Codigo_Tipo_Beneficio { get; set; }
        public String Tipo_Beneficio { get; set; }
        public String RutEmpleador { get; set; }
        public String NombreEmpleador { get; set; }
        public DateTime FechaRecono { get; set; }
        public int Tramo { get; set; }
        public int Monto_Beneficiario { get; set; }
        public  int CodEstTup { get; set; }
        public String GloestTup { get; set; }



    }
}