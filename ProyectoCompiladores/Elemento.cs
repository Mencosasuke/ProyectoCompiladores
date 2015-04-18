using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProyectoCompiladores
{
    class Elemento
    {
        /// <summary>
        /// Instancia de la clase Elemento
        /// </summary>
        /// <param name="valor"></param>
        /// <param name="tipo"></param>
        public Elemento(String valor, TipoDato tipo)
        {
            this.Valor = valor;
            this.Tipo = tipo;
        }
        public Elemento() { }

        // Tipo de dato del elemento { variable, terminal }
        public TipoDato Tipo { get; set; }

        // Valor del elemento
        public String Valor { get; set; }
    }
}
