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
            this.elementos = new List<Elemento>();
        }
        // Lista de elementos que componen la producción
        public IList<Elemento> elementos { get; set; }
    }
}
