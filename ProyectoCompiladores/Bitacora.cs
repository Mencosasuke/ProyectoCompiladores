using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProyectoCompiladores
{
    public partial class Bitacora : Form
    {
        /// <summary>
        /// Valor que representa a Epsilon
        /// </summary>
        private const String IDENTIFICADOR_EPSILON = "e";

        /// <summary>
        /// Declaración de la gramatica sin recursividad
        /// </summary>
        private List<TablaSimbolo> tablaSimbolos;

        public Bitacora(List<TablaSimbolo> tablaSimbolos)
        {
            InitializeComponent();
            this.tablaSimbolos = tablaSimbolos;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCadena_TextChanged(object sender, EventArgs e)
        {
            if (this.txtCadena.Text.Length > 0)
            {
                this.btnValidar.Enabled = true;
            }
            else
            {
                this.btnValidar.Enabled = false;
            }
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            DataTable tblBitacora = new DataTable();
            tblBitacora.Columns.Add("Pila", typeof(String));
            tblBitacora.Columns.Add("Entrada", typeof(String));
            tblBitacora.Columns.Add("Produccion", typeof(String));

            //tblBitacora.Rows.Add("holi", "holi", "holi");

            // Se inicializa la cadena para poder ser tratada como una lista
            List<Char> cadena = new List<Char>(this.txtCadena.Text.ToCharArray());
            cadena.Add('$');

            // Se inicializa la pila
            // El primer elemento apilado será el símbolo $, seguido por la primer variable de la gramatica
            Stack<Elemento> pila = new Stack<Elemento>();
            pila.Push(new Elemento("$", TipoDato.terminal));
            pila.Push(tablaSimbolos[0].Variable);

            // Se inserta el primer ROW
            tblBitacora.Rows.Add(String.Join("  ", pila.Select(p => p.Valor)), String.Join("  ", cadena), String.Empty);

            // Se asignan a variables los datos que se deben conservar por cada iteración
            Elemento ultimaVariable = pila.Peek();
            Produccion ultimaProduccion = new Produccion();

            // Inicia proceso de validación de cadena

            // Caracter de la cadena de entrada que se esta evaluando
            String caracter = String.Empty;
            // Tope de la pila de elementos
            Elemento variable = new Elemento();
            // Posicion que da origen el caracter evaluado para la variable evaluada
            Posicion posicion = new Posicion();
            // Produccion contenida en la posicion
            Produccion produccion = new Produccion();
            // Variable produccion auxiliar a imprimir en columna de producciones
            String produccionImprimir = String.Empty;
            // Flag para saber si la cadena es valida o no
            Boolean flag = false;

            do
            {
                // Se obtienen los primeros elementos de la pila y de la cadena para ser comparados
                caracter = cadena[0].ToString();
                variable = pila.Peek();

                // Si el primer elemento de la pila es un terminal, se valida si su valor es igual al elemento de la cadena
                // que se está evaluando, de ser así, sí y solo sí el valor es diferente al fin de cadena "$", ambos elementos
                // seran eliminados de las pilas, de lo contrario, significa que la cadena finalizo su validacion y es una cadena
                // valida.
                if (variable.Tipo == TipoDato.terminal)
                {
                    // Si ambos tienen el mismo valor se continua evaluando, de lo contrario, la cadena es invalida
                    if (caracter == variable.Valor)
                    {
                        // Si es $, la cadena es valida, de lo contrario, se remueven los datos de las pilas y se continua evaluando
                        // con los siguientes topes de pila.
                        if (caracter == "$")
                        {
                            flag = true;
                            break;
                        }
                        else
                        {
                            pila.Pop();
                            cadena.RemoveAt(0);

                            // Se inserta la nueva fila a la tabla
                            tblBitacora.Rows.Add(String.Join("  ", pila.Select(p => p.Valor)), String.Join("  ", cadena), ultimaVariable.Valor + "  ->  " + String.Join("", ultimaProduccion.Elementos.Select(el => el.Valor)));
                            this.dgvBitacora.DataSource = tblBitacora;

                            continue;
                        }
                    }
                    else
                    {
                        flag = false;
                        break;
                    }
                }

                // Se obtiene la posicion en la tabla que se produce con la variable actual para la terminal evaluada en ese momento
                posicion = tablaSimbolos.Where(ts => ts.Variable.Valor == variable.Valor).FirstOrDefault().Posiciones.Where(p => p.Terminal.Valor == caracter).FirstOrDefault();

                // Se obtiene la primer produccion de dicha posicion
                if (posicion != null)
                {
                    produccion = posicion.Producciones.FirstOrDefault();
                }

                // Si no existe la produccion, la cadena es invalida
                if (produccion == null)
                {
                    flag = false;
                    break;
                }

                // Se desapila la variable ya utilizada y se almacena como la ultima usada
                ultimaVariable = pila.Pop();

                // Se asigna la ultima produccion generada con la variable desapilada
                ultimaProduccion = produccion;

                produccionImprimir = String.Empty;

                // Se apilan todos los elementos de la produccion
                foreach (Elemento ele in produccion.Elementos.Reverse())
                {
                    if (ele.Valor != IDENTIFICADOR_EPSILON)
                    {
                        pila.Push(ele);
                    }
                    else
                    {
                        produccionImprimir = ultimaVariable.Valor + "  ->  " + String.Join("", ultimaProduccion.Elementos.Select(el => el.Valor));
                    }
                }

                // Se inserta la nueva fila a la tabla
                tblBitacora.Rows.Add(String.Join("  ", pila.Select(p => p.Valor)), String.Join("  ", cadena), produccionImprimir ?? "-");
                this.dgvBitacora.DataSource = tblBitacora;

            } while (!flag);

            String resultadoCadena = flag ? "LA CADENA ES VALIDA" : "LA CADENA ES INVALIDA";

            MessageBox.Show(resultadoCadena, "Resultado Validacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            this.dgvBitacora.DataSource = tblBitacora;

            this.btnValidar.Enabled = false;
        }
    }
}
