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
        /// Elimina las comas que se obtienen al concatenar varias cadenas separadas por coma
        /// </summary>
        /// <param name="cadena">Cadena a modificar</param>
        /// <returns>La misma cadena sin la coma de más al final</returns>
        private String eliminarComa(String cadena)
        {
            cadena = cadena.Remove(cadena.Length - 2, 2);
            return cadena;
        }

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

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            this.organizarInformacionGramatica(filePath);
        }

        private void organizarInformacionGramatica(String filePath)
        {
            // Instancia a la clase Gramatica
            Gramatica gramatica = new Gramatica();

            // Se llama al método encargado de obtener cada linea del .txt
            List<String> lineas = gramatica.organizaArchivo(filePath);
            // Se llama al método para obtener cada variable de la gramática
            List<String> variables = gramatica.obtenerVariables(lineas);

            // Se llama al método para obtener las terminales de la gramática
            List<String> terminales = gramatica.obtenerTerminales(lineas);

            // Se llama al método para obtener las producciones por variable de la gramatica
            List<Produccion> producciones = gramatica.obtenerProducciones(lineas);







            // Se imprime la gramatica recursiva en el textbox designado
            this.txtGramaticaRecursiva.Text = String.Join(Environment.NewLine, lineas).Trim();






            // Se arman las cadenas de salida con la información de la gramática organizada

            // String para imprimir las variables de la gramatica
            String lineaVariables = "Variables: ";
            // Se recorren todas las variables obtenidas y se concatenan al String
            foreach (String v in variables)
            {
                lineaVariables += v + ", ";
            }
            // Se remueve la coma del final
            lineaVariables = this.eliminarComa(lineaVariables);



            // String para imprimir las terminales de la gramatica
            String lineaTerminales = "Terminales: ";
            // Se recorren todas las terminales obtenidas y se concatenan al String
            foreach (String t in terminales)
            {
                lineaTerminales += t + ", ";
            }
            // Se remueve la coma del final
            lineaTerminales = this.eliminarComa(lineaTerminales);



            // String para imprimir las producciones de la gramatica
            String lineaProducciones = String.Format("Producciones: {0}", Environment.NewLine);
            foreach (Produccion p in producciones){
                lineaProducciones += String.Format("{0} -> {1}{2}", p.variable, String.Join(", ", p.producciones), Environment.NewLine);
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
            // Se añade linea de producciones
            sbDatosGramatica.AppendLine(lineaProducciones);

            this.txtGramaticaInformacion.Text = sbDatosGramatica.ToString().Trim();
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
