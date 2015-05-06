using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProyectoCompiladores
{
    class Siguiente
    {
        public Siguiente(Elemento variable, IList<Elemento> terminales)
        {
            this.Variable = variable;
            this.Terminales = terminales;
        }
        public Siguiente()
        {
            this.Terminales = new List<Elemento>();
        }

        // Variable de la función siguiente
        public Elemento Variable { get; set; }

        // Lista de terminales que componen la función siguiente de la variable
        public IList<Elemento> Terminales { get; set; }
    }
}
