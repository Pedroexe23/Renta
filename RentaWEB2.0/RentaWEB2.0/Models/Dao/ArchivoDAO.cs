using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentaWEB2._0.Models.Dao
{
    public class ArchivoDAO
    {
        List<Archivos> archivos = new List<Archivos>();
         public void CrearArchivo(Archivos A)
        {
            archivos.Add(A);
        }

         public List<Archivos> GetArchivos()
        {
            return archivos;

        }


    }
}