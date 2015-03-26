using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProyectoCompiladores
{
    class Produccion
    {
        public String variable { get; set; }
        public List<String> producciones { get; set; }

        public Produccion(){
            producciones = new List<String>();
        }
    }
}
