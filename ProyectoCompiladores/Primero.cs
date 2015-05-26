using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProyectoCompiladores
{
    class Primero
    {
        public Primero(Elemento variable, List<Elemento> terminales)
        {
            this.Variable = variable;
            this.Terminales = terminales;
        }
        public Primero()
        {
            this.Terminales = new List<Elemento>();
        }

        // Variable de la función primero
        public Elemento Variable { get; set; }

        // Lista de terminales que componen la función primero de la variable
        public List<Elemento> Terminales { get; set; }
    }
}
