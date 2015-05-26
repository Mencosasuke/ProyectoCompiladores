using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProyectoCompiladores
{
    class Posicion
    {
        public Posicion()
        {
            this.Producciones = new List<Produccion>();
        }

        public Posicion(Elemento _terminal, List<Produccion> _producciones)
        {
            this.Terminal = _terminal;
            this.Producciones = _producciones;
        }

        /// <summary>
        /// Terminal indice de la tabla de simbolos.
        /// </summary>
        public Elemento Terminal { get; set; }

        /// <summary>
        /// Lista de producciones que contienen o producen a la terminal correspondiente en la lista de 
        /// producciones de la variable que se evalua.
        /// </summary>
        public List<Produccion> Producciones { get; set; }

    }
}
