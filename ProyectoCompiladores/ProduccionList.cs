using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProyectoCompiladores
{
    class ProduccionList
    {
        public Elemento variable { get; set; }
        public List<Produccion> producciones { get; set; }

        public ProduccionList(){
            producciones = new List<Produccion>();
        }
    }
}
