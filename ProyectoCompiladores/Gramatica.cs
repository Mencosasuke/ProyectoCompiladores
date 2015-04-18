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
        public List<String> OrganizaArchivo(String filePath){
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
        /// Obtiene todas las variables de la gramática y las almacena en una lista Tipo Elemento
        /// </summary>
        /// <param name="lineas">Lista de las lineas que contiene el archivo .txt</param>
        /// <returns>Lista de las variables</returns>
        public IList<Elemento> ObtenerVariables(List<String> lineas){
            List<Elemento> variables = new List<Elemento>(); // Lista de todas las variables contenidas en el archivo
            foreach (String linea in lineas)
            {
                // Obtiene la subcadena antes de los dos puntos (la variable) y la añade
                // a la lista de variables, sí y solo sí aún no existe en dicha lista
                Elemento variable = new Elemento(linea.Substring(0, linea.IndexOf(":")).Trim(), TipoDato.variable);
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
        public IList<Elemento> ObtenerTerminales(List<String> lineas){
            IList<Elemento> terminales = new List<Elemento>();
            Regex regex = new Regex(@"\'(?<token>[^\|\']+)\'");
            foreach (String l in lineas){
                bool flag = regex.IsMatch(l);
                var result = (from Match m in regex.Matches(l)
                              where m.Groups["token"].Success
                              select m.Groups["token"].Value).ToList();
                foreach (Object o in result){
                    Elemento elemento = new Elemento(o.ToString(), TipoDato.terminal);
                    if (!terminales.Contains(elemento)){
                        terminales.Add(elemento);
                    }
                }
            }
            return terminales;
        }

        /// <summary>
        /// Obtiene las producciones de cada variable
        /// </summary>
        /// <param name="lineas"></param>
        /// <returns></returns>
        public IList<ProduccionList> ObtenerProducciones(List<String> lineas){
            // Lista de producciones
            IList<ProduccionList> producciones = new List<ProduccionList>();
            // Recorre cada linea del txt para obtener las producciones
            foreach (String l in lineas)
            {
                ProduccionList produccion = new ProduccionList();
                // Obtiene la variable de la linea
                Elemento variable = new Elemento(l.Substring(0, l.IndexOf(":")).Trim(), TipoDato.variable);
                // Si la variable ya existe en la lista de producciones, la ignora
                if(!producciones.Any(p => p.variable.Valor.Equals(variable.Valor)))
                {
                    produccion.variable = variable;
                    producciones.Add(produccion);
                }

                // Obtiene de la lista la producción que corresponde a la variable de la lina actual
                ProduccionList produccionAux = producciones.Where(p => p.variable.Valor.Equals(variable.Valor)).FirstOrDefault();

                // Linea auxiliar que obtiene las producciones de la variable de la linea actual
                String lineaAux = l.Split(":".ToCharArray())[1].Trim();

                // Separa las producciones de la linea actual y las guarda en la lista
                // de producciones de la variable a que corresponden
                List<String> splitProducciones = lineaAux.Split("|".ToCharArray()).ToList();
                foreach (String p in splitProducciones)
                {
                    // Lista de Elementos que componen la producción
                    IList<Elemento> elementos = new List<Elemento>();

                    // Arreglo de caracteres que componen la producción
                    Char[] contenidoProduccion = p.Trim().ToCharArray();

                    // Por cada caracter dentro de la producción, verifica si es una variable o un terminal,
                    // si es terminal, lo añade como un elemento nuevo de la lista de Elementos que compone
                    // la pruducción, si no, lee el terminal completo y lo añade como nuevo elemento de la producción.
                    for (int i = 0; i < contenidoProduccion.Length; i++)
                    {
                        char caracter = contenidoProduccion[i];
                        // Si no empieza con comilla simble ' es una variable
                        if (caracter != '\'')
                        {
                            if (caracter != ' ')
                            {
                                Elemento elemento = new Elemento(caracter.ToString(), TipoDato.variable);
                                elementos.Add(elemento);
                            }
                        }
                        else
                        {
                            i++;
                            caracter = contenidoProduccion[i];
                            String valor = String.Empty;
                            do
                            {
                                valor += caracter.ToString();
                                i++;
                                caracter = contenidoProduccion[i];
                            }while(caracter != '\'');
                            Elemento elemento = new Elemento(valor, TipoDato.terminal);
                            elementos.Add(elemento);
                        }
                    }
                    Produccion produccionFinal = new Produccion();
                    produccionFinal.Elementos = elementos;
                    produccionAux.producciones.Add(produccionFinal);
                }
            }
            return producciones;
        }
    }
}
