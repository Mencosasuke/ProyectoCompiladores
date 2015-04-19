﻿namespace ProyectoCompiladores
{
    partial class Form1
    {
        /// <summary>
        /// Required designer Variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.txtGramaticaRecursiva = new System.Windows.Forms.TextBox();
            this.txtGramaticaInformacion = new System.Windows.Forms.TextBox();
            this.btnEjecutar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnInfo = new System.Windows.Forms.Button();
            this.txtGramaticaSinRecursividad = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFuncionesPrimero = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnBuscar
            // 
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.Location = new System.Drawing.Point(302, 7);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(103, 28);
            this.btnBuscar.TabIndex = 0;
            this.btnBuscar.Text = "Buscar Archivo";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtPath
            // 
            this.txtPath.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtPath.Location = new System.Drawing.Point(12, 12);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(270, 20);
            this.txtPath.TabIndex = 1;
            // 
            // txtGramaticaRecursiva
            // 
            this.txtGramaticaRecursiva.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtGramaticaRecursiva.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGramaticaRecursiva.Location = new System.Drawing.Point(12, 74);
            this.txtGramaticaRecursiva.Multiline = true;
            this.txtGramaticaRecursiva.Name = "txtGramaticaRecursiva";
            this.txtGramaticaRecursiva.ReadOnly = true;
            this.txtGramaticaRecursiva.Size = new System.Drawing.Size(270, 276);
            this.txtGramaticaRecursiva.TabIndex = 2;
            // 
            // txtGramaticaInformacion
            // 
            this.txtGramaticaInformacion.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtGramaticaInformacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGramaticaInformacion.Location = new System.Drawing.Point(302, 74);
            this.txtGramaticaInformacion.Multiline = true;
            this.txtGramaticaInformacion.Name = "txtGramaticaInformacion";
            this.txtGramaticaInformacion.ReadOnly = true;
            this.txtGramaticaInformacion.Size = new System.Drawing.Size(270, 276);
            this.txtGramaticaInformacion.TabIndex = 3;
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEjecutar.Enabled = false;
            this.btnEjecutar.Location = new System.Drawing.Point(593, 12);
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(227, 37);
            this.btnEjecutar.TabIndex = 4;
            this.btnEjecutar.Text = "Ejecutar Lectura";
            this.btnEjecutar.UseVisualStyleBackColor = true;
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "GRAMATICA RECURSIVA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(318, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(240, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "INFORMACION OBTENIDA DE LA GRAMATICA";
            // 
            // btnInfo
            // 
            this.btnInfo.BackColor = System.Drawing.Color.Transparent;
            this.btnInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInfo.Image = global::ProyectoCompiladores.Properties.Resources.info_icon;
            this.btnInfo.Location = new System.Drawing.Point(826, 12);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(37, 37);
            this.btnInfo.TabIndex = 8;
            this.btnInfo.UseVisualStyleBackColor = false;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // txtGramaticaSinRecursividad
            // 
            this.txtGramaticaSinRecursividad.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtGramaticaSinRecursividad.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGramaticaSinRecursividad.Location = new System.Drawing.Point(593, 74);
            this.txtGramaticaSinRecursividad.Multiline = true;
            this.txtGramaticaSinRecursividad.Name = "txtGramaticaSinRecursividad";
            this.txtGramaticaSinRecursividad.ReadOnly = true;
            this.txtGramaticaSinRecursividad.Size = new System.Drawing.Size(270, 276);
            this.txtGramaticaSinRecursividad.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(638, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(175, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "GRAMATICA SIN RECURSIVIDAD";
            // 
            // txtFuncionesPrimero
            // 
            this.txtFuncionesPrimero.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtFuncionesPrimero.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFuncionesPrimero.Location = new System.Drawing.Point(12, 382);
            this.txtFuncionesPrimero.Multiline = true;
            this.txtFuncionesPrimero.Name = "txtFuncionesPrimero";
            this.txtFuncionesPrimero.ReadOnly = true;
            this.txtFuncionesPrimero.Size = new System.Drawing.Size(270, 276);
            this.txtFuncionesPrimero.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(93, 366);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "FUNCION PRIMERO";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(875, 670);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFuncionesPrimero);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtGramaticaSinRecursividad);
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEjecutar);
            this.Controls.Add(this.txtGramaticaInformacion);
            this.Controls.Add(this.txtGramaticaRecursiva);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnBuscar);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Proyecto Compiladores - UMG";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.TextBox txtGramaticaRecursiva;
        private System.Windows.Forms.TextBox txtGramaticaInformacion;
        private System.Windows.Forms.Button btnEjecutar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.TextBox txtGramaticaSinRecursividad;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFuncionesPrimero;
        private System.Windows.Forms.Label label4;
    }
}

