using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ProyectoCompiladores
{
    public partial class Form1 : Form
    {
        private String filePath;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento Click del boton para solicitar la busqueda del archivo .txt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            OpenFileDialog archivo = new OpenFileDialog();
            archivo.Filter = "Text Files | *.txt";
            if (archivo.ShowDialog() == DialogResult.OK){
                filePath = archivo.FileName;
                this.txtPath.Text = filePath;
                this.btnEjecutar.Enabled = true;
            }
        }

        /// <summary>
        /// Evento Click para el botón que ejecuta todo el proceso del compilador
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            this.organizarInformacionGramatica(filePath);
        }

        /// <summary>
        /// Método que llama a los métodos respectivos para el procesamiento del compilador.
        /// </summary>
        /// <param name="filePath">Ruta completa de la ubicación del archivo .txt</param>
        private void organizarInformacionGramatica(String filePath)
        {
            // Instancia de la clase Helper
            Helper helper = new Helper();

            // Instancia a la clase Gramatica
            Gramatica gramatica = new Gramatica();

            // Se llama al método encargado de obtener cada linea del .txt
            List<String> lineas = gramatica.OrganizaArchivo(filePath);

            // Se llama al método para obtener cada Variable de la gramática original
            IList<Elemento> variablesOriginales = gramatica.ObtenerVariablesOriginales(lineas);

            // Se llama al método para obtener las terminales originales de la gramática
            IList<Elemento> terminalesOriginales = gramatica.ObtenerTerminalesOriginales(lineas);

            // Se llama al método para obtener las Producciones por Variable de la gramatica
            IList<ProduccionList> produccionesOriginales = gramatica.ObtenerProduccionesOriginales(lineas);

            // Se llama al método para quitar la recursividad
            IList<ProduccionList> gramaticaSinRecursividad = gramatica.QuitarRecursividad(produccionesOriginales);

            // Se llama al método para obtener las variables de la gramatica sin recursividad
            IList<Elemento> variablesSinRecursividad = gramatica.ObtenerVariablesSinRecursividad(gramaticaSinRecursividad);

            // Se llama al método para obtener las terminales de la gramatica sin recursividad
            IList<Elemento> terminalesSinRecursividad = gramatica.ObtenerTerminalesSinRecursividad(gramaticaSinRecursividad);

            // Se llama al método para obtener la función primero de la gramatica sin recursividad
            IList<Primero> listaFuncionesPrimero = gramatica.ObtenerFuncionPrimero(gramaticaSinRecursividad);

            // Se llama al método para obtener la función siguiente de la gramatica sin recursividad
            IList<Siguiente> listaFuncionesSiguiente = gramatica.ObtenerFuncionSiguiente(gramaticaSinRecursividad, listaFuncionesPrimero);

            

            // Se imprime la gramatica recursiva en el textbox designado
            this.txtGramaticaRecursiva.Text = String.Join(Environment.NewLine, lineas).Trim();



            // Se arman las cadenas de salida con la información de la gramática original

            // String para imprimir las variables de la gramatica
            String lineaVariables = "Variables: ";
            lineaVariables += String.Join(", ", variablesOriginales.Select(v => v.Valor));

            // String para imprimir las terminales de la gramatica
            String lineaTerminales = "Terminales: ";
            lineaTerminales += String.Join(", ", terminalesOriginales.Select(t => t.Valor));

            // String para imprimir las Producciones de la gramatica
            String lineaProducciones = String.Format("Gramatica: {0}", Environment.NewLine);
            foreach (ProduccionList p in produccionesOriginales){
                IList<String> produccion = new List<String>();
                foreach (Produccion prod in p.Producciones)
                {
                    produccion.Add(String.Join("", prod.Elementos.Select(e => e.Valor).ToList()));
                }
                lineaProducciones += String.Format("{0} : {1}{2}", p.Variable.Valor, String.Join(" | ", produccion), Environment.NewLine);
            }

            // Se añade toda la información obtenida al StringBuilder Principal
            // Definición del StringBuilder principal
            StringBuilder sbDatosGramatica = new StringBuilder();
            // Se añade linea de variables
            sbDatosGramatica.AppendLine(lineaVariables);
            sbDatosGramatica.AppendLine();
            // Se añade linea de terminales
            sbDatosGramatica.AppendLine(lineaTerminales);
            sbDatosGramatica.AppendLine();
            // Se añade linea de Producciones
            sbDatosGramatica.AppendLine(lineaProducciones);

            this.txtGramaticaInformacion.Text = sbDatosGramatica.ToString().Trim();



            // Se arman las cadenas de salida con la información de la gramática sin recursividad
            
            // Proceso para imprimir las variables de la gramatica sin recursividad
            this.txtGramaticaSinRecursividad.Text += "Variables: ";

            foreach(Elemento variable in variablesSinRecursividad)
            {
                this.txtGramaticaSinRecursividad.Text += variable.Valor + " , ";
                //MessageBox.Show("Variable: " + variable.Valor);
            }

            this.txtGramaticaSinRecursividad.Text = this.txtGramaticaSinRecursividad.Text.Substring(0, this.txtGramaticaSinRecursividad.Text.Length - 3) + Environment.NewLine + Environment.NewLine + "Terminales: ";
            //MessageBox.Show("");

            // Proceso para imprimir las terminales de la gramatica sin recursividad            
            foreach (Elemento terminal in terminalesSinRecursividad)
            {
                this.txtGramaticaSinRecursividad.Text += terminal.Valor + " , ";
                //MessageBox.Show(String.Format("Terminal: ' {0} '", terminal.Valor));
            }

            this.txtGramaticaSinRecursividad.Text = this.txtGramaticaSinRecursividad.Text.Substring(0, this.txtGramaticaSinRecursividad.Text.Length - 3) + Environment.NewLine + Environment.NewLine + "Gramatica: " + Environment.NewLine;
            //MessageBox.Show("");

            // Proceso para imprimir las Producciones de la gramatica sin recursividad
            foreach (ProduccionList p in gramaticaSinRecursividad)
            {
                IList<String> produccion = new List<String>();
                foreach (Produccion prod in p.Producciones)
                {
                    produccion.Add(String.Join("", prod.Elementos.Select(e => e.Valor).ToList()));
                }
                this.txtGramaticaSinRecursividad.Text += p.Variable.Valor + " : ";
                foreach (String prod in produccion)
                {
                    this.txtGramaticaSinRecursividad.Text += prod + " | ";
                    //MessageBox.Show("");
                }
                this.txtGramaticaSinRecursividad.Text = this.txtGramaticaSinRecursividad.Text.Substring(0, this.txtGramaticaSinRecursividad.Text.Length - 3) + Environment.NewLine;
            }



            // Se arma la cadena String para imprimir las funciones primero calculadas

            //Proceso para imprimir las funciones primero
            foreach (Primero funcionPrimero in listaFuncionesPrimero)
            {
                //MessageBox.Show("Funcion primero de : " + this.txtFuncionesPrimero.Text);
                this.txtFuncionesPrimero.Text += "Primero (" + funcionPrimero.Variable.Valor + ")  :  ";
                foreach (Elemento terminal in funcionPrimero.Terminales)
                {
                    this.txtFuncionesPrimero.Text += terminal.Valor + " , ";
                    //MessageBox.Show("Terminal : " + terminal.Valor);
                }
                this.txtFuncionesPrimero.Text = this.txtFuncionesPrimero.Text.Substring(0, this.txtFuncionesPrimero.Text.Length - 3) + Environment.NewLine;
                //MessageBox.Show("");
            }



            // Se arma la cadena String para imprimir las funciones siguiente calculadas

            //Proceso para imprimir las funciones siguiente
            foreach (Siguiente funcionSiguiente in listaFuncionesSiguiente)
            {
                //MessageBox.Show("Funcion siguiente de : " + funcionSiguiente.Variable.Valor);
                this.txtFuncionSiguiente.Text += "Siguiente (" + funcionSiguiente.Variable.Valor + ")  :  ";
                foreach (Elemento terminal in funcionSiguiente.Terminales)
                {
                    this.txtFuncionSiguiente.Text += terminal.Valor + "  ,  ";
                    //MessageBox.Show("Terminal : " + terminal.Valor);
                }
                this.txtFuncionSiguiente.Text = this.txtFuncionSiguiente.Text.Substring(0, this.txtFuncionSiguiente.Text.Length - 3) + Environment.NewLine;
                //MessageBox.Show("");
            }


        }

        /// <summary>
        /// Acción para mostrar la información de alumno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInfo_Click(object sender, EventArgs e)
        {
            String info = String.Join(Environment.NewLine, new String[] { "Universidad Mariano Gálvez de Guatemala",
                                                                          "Facultad de Ingeniería en Sistemas",
                                                                          "Sección \"A\" Diaria Vespertina",
                                                                          "Complidaroes",
                                                                          "Ing. Corina Pérez",
                                                                          "Primera Frase Proyecto",
                                                                          "David Fernando Mencos García",
                                                                          "090-13-9241"
                                                                        });
            MessageBox.Show(info, "Información del desarrollador", MessageBoxButtons.OK);
        }
    }
}
