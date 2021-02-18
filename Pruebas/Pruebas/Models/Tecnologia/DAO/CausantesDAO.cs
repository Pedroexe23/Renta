using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pruebas.Models.Tecnologia.DAO
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
    }
}