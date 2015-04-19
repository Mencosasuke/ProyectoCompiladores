﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProyectoCompiladores
{
    class ProduccionList
    {
        /// <summary>
        /// Instancia de la calse
        /// </summary>
        public ProduccionList()
        {
            Producciones = new List<Produccion>();
        }
        /// <summary>
        /// Variable que produce n lineaProducciones.
        /// </summary>
        public Elemento Variable { get; set; }

        /// <summary>
        /// Lista de lineaProducciones que se producen por la variable
        /// </summary>
        public IList<Produccion> Producciones { get; set; }
    }
}
