using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentaWEB2._0.Models.Dao
{
    public class DocumentoDAO
    {
        private static List<Documento> Documentos = new List<Documento>();

        public void Creardocumento( Documento documento)
        {
            Documentos.Add(documento);

        }

        public List<Documento> GetDocumentos()
        {
            return Documentos;
        }

        public void EliminarDocumento()
        {
            List<Documento> limpiar = GetDocumentos();

            for (int i = 0; i >= limpiar.Count; i++)
            {
                if (limpiar.Count!=0)
                {

                

                Documento documentos = new Documento();
                documentos.Archivo = limpiar[i].Archivo;
                documentos.Tamaño = limpiar[i].Tamaño;
                documentos.tipo = limpiar[i].tipo;
                documentos.Fecha = limpiar[i].Fecha;

                Documentos.Remove(documentos);
                }
            }
        }


    }
}