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
        public IList<Elemento> ObtenerVariablesOriginales(List<String> lineas){
            List<Elemento> variables = new List<Elemento>(); // Lista de todas las variables contenidas en el archivo
            foreach (String linea in lineas)
            {
                // Obtiene la subcadena antes de los dos puntos (la Variable) y la añade
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
        public IList<Elemento> ObtenerTerminalesOriginales(List<String> lineas){
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
        /// Obtiene las Producciones de cada Variable
        /// </summary>
        /// <param name="lineas">Lineas del archivo de la gramatica</param>
        /// <returns>La gramatica ordenada y organizada</returns>
        public IList<ProduccionList> ObtenerProduccionesOriginales(List<String> lineas){
            // Lista de Producciones
            IList<ProduccionList> producciones = new List<ProduccionList>();
            // Recorre cada linea del txt para obtener las Producciones
            foreach (String l in lineas)
            {
                ProduccionList produccion = new ProduccionList();
                // Obtiene la Variable de la linea
                Elemento variable = new Elemento(l.Substring(0, l.IndexOf(":")).Trim(), TipoDato.variable);
                // Si la Variable ya existe en la lista de Producciones, la ignora
                if(!producciones.Any(p => p.Variable.Valor.Equals(variable.Valor)))
                {
                    produccion.Variable = variable;
                    producciones.Add(produccion);
                }

                // Obtiene de la lista la producción que corresponde a la Variable de la lina actual
                ProduccionList produccionAux = producciones.Where(p => p.Variable.Valor.Equals(variable.Valor)).FirstOrDefault();

                // Linea auxiliar que obtiene las Producciones de la Variable de la linea actual
                String lineaAux = l.Split(":".ToCharArray())[1].Trim();

                // Separa las Producciones de la linea actual y las guarda en la lista
                // de Producciones de la Variable a que corresponden
                List<String> splitProducciones = lineaAux.Split("|".ToCharArray()).ToList();
                foreach (String p in splitProducciones)
                {
                    // Lista de Elementos que componen la producción
                    IList<Elemento> elementos = new List<Elemento>();

                    // Arreglo de caracteres que componen la producción
                    Char[] contenidoProduccion = p.Trim().ToCharArray();

                    // Por cada caracter dentro de la producción, verifica si es una Variable o un terminal,
                    // si es terminal, lo añade como un elemento nuevo de la lista de Elementos que compone
                    // la pruducción, si no, lee el terminal completo y lo añade como nuevo elemento de la producción.
                    for (int i = 0; i < contenidoProduccion.Length; i++)
                    {
                        char caracter = contenidoProduccion[i];
                        // Si no empieza con comilla simble ' es una Variable
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
                    produccionAux.Producciones.Add(produccionFinal);
                }
            }
            return producciones;
        }

        /// <summary>
        /// Metodo para quitar la recursividad de la gramatica.
        /// </summary>
        /// <param name="listaProducciones">Lista de variable : Produccion(es) de la gramatica</param>
        /// <returns>La nueva gramatica sin recursividad</returns>
        public IList<ProduccionList> QuitarRecursividad(IList<ProduccionList> listaProducciones)
        {
            // Nueva lista sin recursividad por la izquierda
            IList<ProduccionList> gramaticaSinRecursividad = new List<ProduccionList>();

            // Recorre todas las lineas de producciones de la gramatica
            foreach (ProduccionList lineaProducciones in listaProducciones)
            {
                Elemento variablePrima = new Elemento(String.Format("{0}1", lineaProducciones.Variable.Valor), TipoDato.variable);

                IList<Produccion> alfas = new List<Produccion>();
                IList<Produccion> betas = new List<Produccion>();

                ProduccionList newListaProduccionesBeta = new ProduccionList();
                newListaProduccionesBeta.Variable = lineaProducciones.Variable;

                ProduccionList newListaProduccionesAlfa = new ProduccionList();
                newListaProduccionesAlfa.Variable = variablePrima;

                // Recorre cada produccion por variable
                foreach (Produccion produccion in lineaProducciones.Producciones)
                {
                    // Si la produccion tiene recursividad por la izquierda, obtiene alfa y la añade a lista de alfas,
                    // de lo contrario, añade la producción a la lista de betas.
                    if (produccion.Elementos.FirstOrDefault().Valor.Equals(lineaProducciones.Variable.Valor))
                    {
                        Produccion newAlfa = new Produccion();
                        
                        // Recorre los elementos siguientes a la variable recursiva para obtener ALFA y añade ALFA a la
                        // lista de ALFAS
                        for (int i = 1; i < produccion.Elementos.Count; i++)
                        {
                            newAlfa.Elementos.Add(produccion.Elementos[i]);
                        }
                        newAlfa.Elementos.Add(variablePrima);
                        alfas.Add(newAlfa);
                    }
                    else
                    {
                        Produccion newBeta = new Produccion();

                        // De lo contrario, añade la lista de elementos como una producción BETA a la lista de BETAS y 
                        // añade la variable prima al final de la producción
                        foreach (Elemento elemento in produccion.Elementos)
                        {
                            newBeta.Elementos.Add(elemento);
                        }
                        if (alfas.Count > 0)
                        {
                            newBeta.Elementos.Add(variablePrima);
                        }
                        betas.Add(newBeta);
                    }
                }

                // Añade las producciones beta a la nueva linea de producciones beta de esa variable
                newListaProduccionesBeta.Producciones = betas;

                // Añade las nuevas lineas de producciones a la gramatica sin recursividad
                gramaticaSinRecursividad.Add(newListaProduccionesBeta);

                // Añade las producciones alfa si la produccion es recursiva por la izquierda
                if (alfas.Count > 0)
                {
                    // Añade Epsilon a la lista de Alfas si la variable es recursiva
                    IList<Elemento> elementoEpsilon = new List<Elemento>();
                    elementoEpsilon.Add(new Elemento("Ep", TipoDato.terminal));
                    alfas.Add(new Produccion(elementoEpsilon));

                    // Añade las producciones alfa a la nueva linea de producciones alfa de la variable prima
                    newListaProduccionesAlfa.Producciones = alfas;

                    // Añade las nuevas lineas de producciones a la gramatica sin recursividad
                    gramaticaSinRecursividad.Add(newListaProduccionesAlfa);
                }
            }

            return gramaticaSinRecursividad;
        }

        /// <summary>
        /// Obtiene todas las variables de la nueva gramatica sin recursividad
        /// </summary>
        /// <param name="listaProducciones">Gramatica sin recursividad</param>
        /// <returns>Lista de variables de la gramatica sin recursividad</returns>
        public IList<Elemento> ObtenerVariablesSinRecursividad(IList<ProduccionList> listaProducciones)
        {
            IList<Elemento> listaVariables = new List<Elemento>();

            // Recorre cada linea de variable : produccion(es) para obtener todas las variables
            foreach (ProduccionList lineaProducciones in listaProducciones)
            {
                // Si la variable no existe en la lista, la agrega
                if (!listaVariables.Contains(lineaProducciones.Variable))
                {
                    listaVariables.Add(lineaProducciones.Variable);
                }
            }

            return listaVariables;
        }

        /// <summary>
        /// Obtiene todas las terminales de la nueva gramatica sin recursividad
        /// </summary>
        /// <param name="listaProducciones">Gramatica sin recursividad</param>
        /// <returns>Lista de terminales de la gramatica sin recursividad</returns>
        public IList<Elemento> ObtenerTerminalesSinRecursividad(IList<ProduccionList> listaProducciones)
        {
            IList<Elemento> listaTerminales = new List<Elemento>();

            // Recorre cada linea de variable : produccion(es) para obtener todas las terminales
            foreach (ProduccionList lineaProducciones in listaProducciones)
            {
                // Recorre cada produccion de dicha linea para obtener las terminales
                foreach (Produccion produccion in lineaProducciones.Producciones)
                {
                    // Recorre cada elemento de la produccion y verifica si es terminal, de ser así, sí y solo sí no
                    // existe en la lista de terminales, se añade a la lista.
                    foreach (Elemento elemento in produccion.Elementos)
                    {
                        if (elemento.Tipo == TipoDato.terminal && !listaTerminales.Any(l => l.Valor == elemento.Valor))
                        {
                            listaTerminales.Add(elemento);
                        }
                    }
                }
            }

            return listaTerminales;
        }

        /// <summary>
        /// Devuelve las funciones primero calculadas de cada variable
        /// </summary>
        /// <param name="listaProducciones">gramatica sin recursividad</param>
        /// <returns>Lista de las funciones primero calculadas de cada variable</returns>
        public IList<Primero> ObtenerFuncionPrimero(IList<ProduccionList> listaProducciones)
        {
            IList<Primero> listaFuncionesPrimero = new List<Primero>();

            foreach (ProduccionList lineaProduccion in listaProducciones)
            {
                IList<Elemento> elementos = new List<Elemento>();
                this.CalcularFuncionPrimero(lineaProduccion, listaProducciones, ref elementos);
                Primero produccionPrimero = new Primero(lineaProduccion.Variable, elementos);
                listaFuncionesPrimero.Add(produccionPrimero);
            }

            return listaFuncionesPrimero;
        }

        /// <summary>
        /// Calcula la función primero de una variable en específico
        /// </summary>
        /// <param name="lineaProduccion">Linea de producciones de la variable que se calcula</param>
        /// <param name="listaProducciones">Gramatica sin recursividad</param>
        /// <param name="elementos">Lista de elementos de la funcion primero para dicha variable</param>
        public void CalcularFuncionPrimero(ProduccionList lineaProduccion, IList<ProduccionList> listaProducciones, ref IList<Elemento> elementos)
        {
            // Recorre cada produccion de la linea de producciones de la variable actual
            foreach (Produccion produccion in lineaProduccion.Producciones)
            {
                Elemento primerElemento = produccion.Elementos[0];

                // Si el primer elemento de la produccion es un terminal, lo añade a la lista de elementos de la función primero,
                // sí y solo sí, este elemento no existe en la lista. De lo contrario, el elemento es una variable y al ser así,
                // se llama a este mismo método para calcular la función primero de esa variable
                if (primerElemento.Tipo == TipoDato.terminal)
                {
                    if (!elementos.Any(e => e.Valor.Equals(primerElemento.Valor)))
                    {
                        elementos.Add(primerElemento);
                    }
                }
                else
                {
                    lineaProduccion = listaProducciones.Where(lp => lp.Variable.Valor.Equals(primerElemento.Valor)).FirstOrDefault();
                    this.CalcularFuncionPrimero(lineaProduccion, listaProducciones, ref elementos);
                }
            }
        }

        public IList<Siguiente> ObtenerFuncionSiguiente(IList<ProduccionList> listaProducciones, IList<Primero> listaFuncionesPrimero)
        {
            IList<Siguiente> listaFuncionesSiguiente = new List<Siguiente>();

            foreach (Elemento variable in listaProducciones.Select(lp => lp.Variable).ToList())
            {
                Siguiente siguiente = new Siguiente();
                List<Elemento> elementos = new List<Elemento>();
                siguiente.Variable = variable;
                this.CalcularFuncionSiguiente(variable, listaProducciones, listaFuncionesPrimero, ref elementos);
                elementos.Add(new Elemento("$", TipoDato.terminal));
                siguiente.Terminales = elementos;
                listaFuncionesSiguiente.Add(siguiente);
            }

            return listaFuncionesSiguiente;
        }

        public void CalcularFuncionSiguiente(Elemento variable, IList<ProduccionList> listaProducciones, IList<Primero> listaFuncionesPrimero, ref List<Elemento> elementos)
        {
            foreach (ProduccionList lineaProduccion in listaProducciones)
            {
                foreach (Produccion produccion in lineaProduccion.Producciones)
                {
                    if (produccion.Elementos.Any(e => e.Valor == variable.Valor))
                    {
                        int longitud = produccion.Elementos.Count;
                        for (int i = 0; i < longitud; i++)
                        {
                            if (produccion.Elementos[i].Valor == variable.Valor)
                            {
                                // Si siguiente es Epsilon, llama a función siguiente de la variable actual
                                if (i + 1 > longitud - 1)
                                {
                                    // Si la variable que se esta calculando es igual a la variable actual, no hace nada,
                                    // de lo contrario, calcula siguiente de esa variable y añade a lista de elementos.
                                    if (variable.Valor != lineaProduccion.Variable.Valor)
                                    {
                                        this.CalcularFuncionSiguiente(lineaProduccion.Variable, listaProducciones, listaFuncionesPrimero, ref elementos);
                                    }
                                }
                                // Si siguiente es terminal, la añade a la lista de elementos, si no existe.
                                else if(produccion.Elementos[i + 1].Tipo == TipoDato.terminal)
                                {
                                    if (!elementos.Any(e => e.Valor == produccion.Elementos[i + 1].Valor))
                                    {
                                        elementos.Add(produccion.Elementos[i + 1]);
                                    }
                                }
                                // Si siguiente es variable, llama a función primero de esa variable
                                else if (produccion.Elementos[i + 1].Tipo == TipoDato.variable)
                                {
                                    Primero primero = new Primero();
                                    primero = listaFuncionesPrimero.Where(fp => fp.Variable.Valor == produccion.Elementos[i + 1].Valor).FirstOrDefault();
                                    foreach (Elemento elemento in primero.Terminales)
                                    {
                                        if (!elementos.Any(e => e.Valor == elemento.Valor) && elemento.Valor != "Ep")
                                        {
                                            elementos.Add(elemento);
                                        }
                                        else if (elemento.Valor == "Ep")
                                        {
                                            this.CalcularFuncionSiguiente(primero.Variable, listaProducciones, listaFuncionesPrimero, ref elementos);
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
