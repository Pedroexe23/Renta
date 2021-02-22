using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentaWEB2._0.Models.Dao
{
    public class DocumentoDAO
    {
        List<Documento> documentos = new List<Documento>();

        public void Crear(Documento d)
        {
            documentos.Add(d);
        }
        public List<Documento> GetCausantes()
        {
            return documentos;
        }
        public void EliminarLista(Documento d)
        {
            documentos.Remove(d);
            
        }


    }
}