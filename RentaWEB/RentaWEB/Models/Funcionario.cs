using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentaWEB.Models
{
    public class Funcionario
    {
       public int Id_Funcionario { get; set; }
       public int Activo { get; set;}
       public String Rut { get; set; }
        public String Apellido { get; set; }
        public String Nombre { get; set; }
        public int Sexo { get; set; }
        public int EstadoCivil { get; set; }
        public DateTime Fec_nacimiento { get; set; }
        public String Direccion { get; set; }
        public int RentaPromedio { get; set; }


    }
}