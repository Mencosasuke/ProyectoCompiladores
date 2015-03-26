using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ProyectoCompiladores
{
    class Gramatica
    {
        /// <summary>
        /// Lee el archivo y guarda cada linea del mismo en una lista de strings.
        /// </summary>
        /// <param name="filePath">Ruta del archivo a leer.</param>
        /// <returns>Retorna la lista de lineas obtenidas del archivo.</returns>
        public List<String> organizaArchivo(String filePath){
            try{
                String linea;
                List<String> lineas = new List<String>();
                using (StreamReader sr = new StreamReader(filePath)){
                    while ((linea = sr.ReadLine()) != null){
                        lineas.Add(linea);
                        //MessageBox.Show(linea);
                    }
                    return lineas;
                }
            }catch (Exception e){
                MessageBox.Show(String.Format("Error al leer archivo. {0}", e.Message), "Error de lectura", MessageBoxButtons.OK);
            }
            return null;
        }

        /// <summary>
        /// Obtiene todas las variables de la gramática y las almacena en una lista tipo String
        /// </summary>
        /// <param name="lineas">Lista de las lineas que contiene el archivo .txt</param>
        /// <returns>Lista de las variables</returns>
        public List<String> obtenerVariables(List<String> lineas){
            List<String> variables = new List<String>(); // Lista de todas las variables contenidas en el archivo
            foreach (String linea in lineas)
            {
                // Obtiene la subcadena antes de los dos puntos (la variable) y la añade
                // a la lista de variables, sí y solo sí aún no existe en dicha lista
                String variable = linea.Substring(0, linea.IndexOf(":")).Trim();
                if(!variables.Contains(variable)){
                    variables.Add(variable);
                }
            }

            return variables;
        }

        /// <summary>
        /// Obtiene todas las terminales de la gramática
        /// </summary>
        /// <param name="lineas">Lista de las lineas que contiene el archivo .txt</param>
        /// <returns>Lista de las terminales</returns>
        public List<String> obtenerTerminales(List<String> lineas){
            List<String> terminales = new List<String>();
            Regex regex = new Regex(@"\'(?<token>[^\|\']+)\'");
            foreach (String l in lineas){
                bool flag = regex.IsMatch(l);
                var result = (from Match m in regex.Matches(l)
                              where m.Groups["token"].Success
                              select m.Groups["token"].Value).ToList();
                foreach (Object o in result){
                    if (!terminales.Contains(o.ToString())){
                        terminales.Add(o.ToString());
                    }
                }
            }
            return terminales;
        }

        public List<Produccion> obtenerProducciones(List<String> lineas){
            // Lista de producciones
            List<Produccion> producciones = new List<Produccion>();
            // Recorre cada linea del txt para obtener las producciones
            foreach (String l in lineas){
                Produccion produccion = new Produccion();
                // Obtiene la variable de la linea
                String variable = l.Substring(0, l.IndexOf(":")).Trim();
                // Si la variable ya existe en la lista de producciones, la ignora
                if(!producciones.Exists(p => p.variable.Equals(variable))){
                    produccion.variable = variable;
                    producciones.Add(produccion);
                }

                // Obtiene de la lista la producción que corresponde a la variable de la lina actual
                Produccion produccionAux = producciones.Where(p => p.variable.Equals(variable)).FirstOrDefault();

                // Linea auxiliar que obtiene las producciones de la variable de la linea actual
                String lineaAux = l.Split(":".ToCharArray())[1].Trim();

                // Separa las producciones de la linea actual y las guarda en la lista
                // de producciones de la variable a que corresponden
                List<String> splitProducciones = lineaAux.Split("|".ToCharArray()).ToList();
                foreach (String p in splitProducciones){
                    produccionAux.producciones.Add(p);
                }
            }
            return producciones;
        }
    }
}
