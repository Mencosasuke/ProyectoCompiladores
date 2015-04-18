using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProyectoCompiladores
{
    class Helper
    {
        /// <summary>
        /// Elimina las comas que se obtienen al concatenar varias cadenas separadas por coma
        /// </summary>
        /// <param name="cadena">Cadena a modificar</param>
        /// <returns>La misma cadena sin la coma de más al final</returns>
        public String eliminarComa(String cadena)
        {
            cadena = cadena.Remove(cadena.Length - 2, 2);
            return cadena;
        }
    }
}
