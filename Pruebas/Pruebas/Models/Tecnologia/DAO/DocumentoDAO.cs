using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pruebas.Models.Tecnologia.DAO
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
    }
}