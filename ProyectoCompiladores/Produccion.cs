using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProyectoCompiladores
{
    class Produccion
    {
        public Produccion()
        {
            this.Elementos = new List<Elemento>();
        }
        // Lista de Elementos que componen la producción
        public IList<Elemento> Elementos { get; set; }
    }
}
