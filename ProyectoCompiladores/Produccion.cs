using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProyectoCompiladores
{
    public class Produccion
    {
        /// <summary>
        /// Instancia de la calse
        /// </summary>
        /// <param name="elementos"></param>
        public Produccion(IList<Elemento> elementos)
        {
            this.Elementos = elementos;
        }
        public Produccion()
        {
            this.Elementos = new List<Elemento>();
        }

        // Lista de Elementos que componen la producción
        public IList<Elemento> Elementos { get; set; }
    }
}
