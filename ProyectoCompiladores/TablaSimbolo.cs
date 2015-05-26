using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProyectoCompiladores
{
    class TablaSimbolo
    {

        public TablaSimbolo()
        {
            this.Posiciones = new List<Posicion>();
        }

        public TablaSimbolo(Elemento _variable, List<Posicion> _posiciones)
        {
            this.Variable = _variable;
            this.Posiciones = _posiciones;
        }

        /// <summary>
        /// Variable indice de la tabla de símbolos
        /// </summary>
        public Elemento Variable { get; set; }

        /// <summary>
        /// Coleccion de producciones por terminal contenidas en la tabla de simbolos.
        /// </summary>
        public List<Posicion> Posiciones { get; set; }

    }
}
