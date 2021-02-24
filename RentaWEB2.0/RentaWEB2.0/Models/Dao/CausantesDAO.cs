using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentaWEB2._0.Models.Dao
{
    public class CausantesDAO
    {
        private static List<Causante> causantes = new List<Causante>();

        public void Crear(Causante c)
        {
            causantes.Add(c);
        }
        public List<Causante> GetCausantes()
        {
            return causantes;
        }
        public void EliminarCausante(Causante c)

        {
            causantes.Remove(c);
        }



        public void EliminarCausantes()
        {
            List<Causante> Limpiar = GetCausantes();
            
                for (int i = 0; i >= Limpiar.Count(); i++)
            {
                if (Limpiar.Count!=0)
                {
                    Causante c = new Causante();
                    c.NUM_CORRELATIVO = Limpiar[i].NUM_CORRELATIVO;
                    c.RUT_CAUSANTE = Limpiar[i].RUT_CAUSANTE;
                    c.NOMBRE_CAUSANTE = Limpiar[i].NOMBRE_CAUSANTE;
                    c.CODIGO_TIPO_CAUSANTE = Limpiar[i].CODIGO_TIPO_CAUSANTE;
                    c.TIPO_CAUSANTE = Limpiar[i].TIPO_CAUSANTE;
                    c.RUT_BENEFICIARIO = Limpiar[i].RUT_BENEFICIARIO;
                    c.NOMBRE_BENEFICIARIO = Limpiar[i].NOMBRE_BENEFICIARIO;
                    c.CODIGO_TIPO_BENEFICIARIO = Limpiar[i].CODIGO_TIPO_BENEFICIARIO;
                    c.TIPO_BENEFICIARIO = Limpiar[i].TIPO_BENEFICIARIO;
                    c.CODIGO_TIPO_BENEFICIO = Limpiar[i].CODIGO_TIPO_BENEFICIO;
                    c.TIPO_BENEFICIO = Limpiar[i].TIPO_BENEFICIO;
                    c.RUT_EMPLEADOR = Limpiar[i].RUT_EMPLEADOR;
                    c.NOMBRE_EMPLEADOR = Limpiar[i].NOMBRE_EMPLEADOR;
                    c.FECHA_RECONOCIMIENTO = Limpiar[i].FECHA_RECONOCIMIENTO;
                    c.TRAMO = Limpiar[i].TRAMO;
                    c.MONTO_BENEFICIO = Limpiar[i].MONTO_BENEFICIO;
                    c.CODIGO_ESTADO_TUPLA = Limpiar[i].CODIGO_ESTADO_TUPLA;
                    c.GLOSA_ESTADO_TUPLA = Limpiar[i].GLOSA_ESTADO_TUPLA;
                    c.PROMEDIO_RENTA = Limpiar[i].PROMEDIO_RENTA;
                    causantes.Remove(c);

                }

            }
            
        }
    } 
}
