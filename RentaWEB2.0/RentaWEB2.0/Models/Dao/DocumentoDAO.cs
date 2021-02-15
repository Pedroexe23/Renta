using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentaWEB2._0.Models.Dao
{
    public class DocumentoDAO
    {
        List<Documentos> documentos = new List<Documentos>();
        
          public void Crear (Documentos d)
        {
            documentos.Add(d);
        }
         public List<Documentos> GetCausantes()
        {
            return documentos;  
        }
         
    }
}