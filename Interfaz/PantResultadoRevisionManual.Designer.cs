namespace PPAI2025.Interfaz
{
    partial class PantResultadoRevisionManual
    {
        /// <summary>
        /// Required designer variable.
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
            this.dataGridEventos = new System.Windows.Forms.DataGridView();
            this.FechaOcurrencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LatitudEpicentro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LongitudEpicentro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LatitudHipocentro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LongitudHipocentro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Magnitud = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblClickEvento = new System.Windows.Forms.Label();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.btnRechazar = new System.Windows.Forms.Button();
            this.btnRevisionExperto = new System.Windows.Forms.Button();
            this.btnVisualizarMapa = new System.Windows.Forms.Button();
            this.btnSismograma = new System.Windows.Forms.Button();
            this.treeAgrupados = new System.Windows.Forms.TreeView();
            this.lblEventos = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEventos)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridEventos
            // 
            this.dataGridEventos.AllowUserToAddRows = false;
            this.dataGridEventos.AllowUserToDeleteRows = false;
            this.dataGridEventos.AllowUserToResizeColumns = false;
            this.dataGridEventos.AllowUserToResizeRows = false;
            this.dataGridEventos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridEventos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridEventos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FechaOcurrencia,
            this.LatitudEpicentro,
            this.LongitudEpicentro,
            this.LatitudHipocentro,
            this.LongitudHipocentro,
            this.Magnitud});
            this.dataGridEventos.Location = new System.Drawing.Point(7, 33);
            this.dataGridEventos.Name = "dataGridEventos";
            this.dataGridEventos.ReadOnly = true;
            this.dataGridEventos.RowHeadersWidth = 82;
            this.dataGridEventos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridEventos.Size = new System.Drawing.Size(721, 289);
            this.dataGridEventos.TabIndex = 0;
            this.dataGridEventos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tomarSeleccionEvento_CellContentClick);
            // 
            // FechaOcurrencia
            // 
            this.FechaOcurrencia.DataPropertyName = "FechaOcurrencia";
            this.FechaOcurrencia.HeaderText = "Fecha de Ocurrencia";
            this.FechaOcurrencia.MinimumWidth = 10;
            this.FechaOcurrencia.Name = "FechaOcurrencia";
            this.FechaOcurrencia.ReadOnly = true;
            this.FechaOcurrencia.Width = 121;
            // 
            // LatitudEpicentro
            // 
            this.LatitudEpicentro.DataPropertyName = "LatitudEpicentro";
            this.LatitudEpicentro.HeaderText = "Latitud Epicentro";
            this.LatitudEpicentro.MinimumWidth = 10;
            this.LatitudEpicentro.Name = "LatitudEpicentro";
            this.LatitudEpicentro.ReadOnly = true;
            this.LatitudEpicentro.Width = 103;
            // 
            // LongitudEpicentro
            // 
            this.LongitudEpicentro.DataPropertyName = "LongitudEpicentro";
            this.LongitudEpicentro.HeaderText = "Longitud Epicentro";
            this.LongitudEpicentro.MinimumWidth = 10;
            this.LongitudEpicentro.Name = "LongitudEpicentro";
            this.LongitudEpicentro.ReadOnly = true;
            this.LongitudEpicentro.Width = 111;
            // 
            // LatitudHipocentro
            // 
            this.LatitudHipocentro.DataPropertyName = "LatitudHipocentro";
            this.LatitudHipocentro.HeaderText = "Latitud Hipocentro";
            this.LatitudHipocentro.MinimumWidth = 10;
            this.LatitudHipocentro.Name = "LatitudHipocentro";
            this.LatitudHipocentro.ReadOnly = true;
            this.LatitudHipocentro.Width = 109;
            // 
            // LongitudHipocentro
            // 
            this.LongitudHipocentro.DataPropertyName = "LongitudHipocentro";
            this.LongitudHipocentro.HeaderText = "Longitud Hipocentro";
            this.LongitudHipocentro.MinimumWidth = 10;
            this.LongitudHipocentro.Name = "LongitudHipocentro";
            this.LongitudHipocentro.ReadOnly = true;
            this.LongitudHipocentro.Width = 117;
            // 
            // Magnitud
            // 
            this.Magnitud.DataPropertyName = "ValorMagnitud";
            this.Magnitud.HeaderText = "Magnitud";
            this.Magnitud.MinimumWidth = 10;
            this.Magnitud.Name = "Magnitud";
            this.Magnitud.ReadOnly = true;
            this.Magnitud.Width = 76;
            // 
            // lblClickEvento
            // 
            this.lblClickEvento.Location = new System.Drawing.Point(0, 0);
            this.lblClickEvento.Name = "lblClickEvento";
            this.lblClickEvento.Size = new System.Drawing.Size(100, 23);
            this.lblClickEvento.TabIndex = 0;
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.Color.LimeGreen;
            this.btnConfirmar.Enabled = false;
            this.btnConfirmar.Location = new System.Drawing.Point(733, 33);
            this.btnConfirmar.Margin = new System.Windows.Forms.Padding(2);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(110, 60);
            this.btnConfirmar.TabIndex = 1;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnRechazar
            // 
            this.btnRechazar.BackColor = System.Drawing.Color.IndianRed;
            this.btnRechazar.Enabled = false;
            this.btnRechazar.Location = new System.Drawing.Point(733, 97);
            this.btnRechazar.Margin = new System.Windows.Forms.Padding(2);
            this.btnRechazar.Name = "btnRechazar";
            this.btnRechazar.Size = new System.Drawing.Size(110, 60);
            this.btnRechazar.TabIndex = 2;
            this.btnRechazar.Text = "Rechazar";
            this.btnRechazar.UseVisualStyleBackColor = false;
            this.btnRechazar.Click += new System.EventHandler(this.tomarIngresoSeleccionAccion_Click);
            // 
            // btnRevisionExperto
            // 
            this.btnRevisionExperto.BackColor = System.Drawing.Color.Yellow;
            this.btnRevisionExperto.Enabled = false;
            this.btnRevisionExperto.Location = new System.Drawing.Point(733, 161);
            this.btnRevisionExperto.Margin = new System.Windows.Forms.Padding(2);
            this.btnRevisionExperto.Name = "btnRevisionExperto";
            this.btnRevisionExperto.Size = new System.Drawing.Size(110, 60);
            this.btnRevisionExperto.TabIndex = 3;
            this.btnRevisionExperto.Text = "Solicitar Revision Experto";
            this.btnRevisionExperto.UseVisualStyleBackColor = false;
            this.btnRevisionExperto.Click += new System.EventHandler(this.btnRevisionExperto_Click);
            // 
            // btnVisualizarMapa
            // 
            this.btnVisualizarMapa.Enabled = false;
            this.btnVisualizarMapa.Location = new System.Drawing.Point(733, 229);
            this.btnVisualizarMapa.Margin = new System.Windows.Forms.Padding(2);
            this.btnVisualizarMapa.Name = "btnVisualizarMapa";
            this.btnVisualizarMapa.Size = new System.Drawing.Size(110, 44);
            this.btnVisualizarMapa.TabIndex = 4;
            this.btnVisualizarMapa.Text = "Visualizar Mapa";
            this.btnVisualizarMapa.UseVisualStyleBackColor = true;
            this.btnVisualizarMapa.Click += new System.EventHandler(this.solicitarOpcionVisualizarMapa_Click);
            // 
            // btnSismograma
            // 
            this.btnSismograma.Enabled = false;
            this.btnSismograma.Location = new System.Drawing.Point(733, 277);
            this.btnSismograma.Margin = new System.Windows.Forms.Padding(2);
            this.btnSismograma.Name = "btnSismograma";
            this.btnSismograma.Size = new System.Drawing.Size(110, 45);
            this.btnSismograma.TabIndex = 5;
            this.btnSismograma.Text = "Generar Sismograma";
            this.btnSismograma.UseVisualStyleBackColor = true;
            this.btnSismograma.Click += new System.EventHandler(this.btnSismograma_Click);
            // 
            // treeAgrupados
            // 
            this.treeAgrupados.Location = new System.Drawing.Point(749, 416);
            this.treeAgrupados.Margin = new System.Windows.Forms.Padding(2);
            this.treeAgrupados.Name = "treeAgrupados";
            this.treeAgrupados.Size = new System.Drawing.Size(143, 89);
            this.treeAgrupados.TabIndex = 6;
            this.treeAgrupados.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeAgrupados_AfterSelect);
            // 
            // lblEventos
            // 
            this.lblEventos.AutoSize = true;
            this.lblEventos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEventos.Location = new System.Drawing.Point(12, 9);
            this.lblEventos.Name = "lblEventos";
            this.lblEventos.Size = new System.Drawing.Size(273, 16);
            this.lblEventos.TabIndex = 7;
            this.lblEventos.Text = "Seleccione un Evento a bloquear de la grilla:";
            // 
            // PantResultadoRevisionManual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 334);
            this.Controls.Add(this.lblEventos);
            this.Controls.Add(this.treeAgrupados);
            this.Controls.Add(this.btnSismograma);
            this.Controls.Add(this.btnVisualizarMapa);
            this.Controls.Add(this.btnRevisionExperto);
            this.Controls.Add(this.btnRechazar);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.dataGridEventos);
            this.Name = "PantResultadoRevisionManual";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ResultadoRevisionManual";
            this.Load += new System.EventHandler(this.Principal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEventos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridEventos;
        private System.Windows.Forms.Label lblClickEvento;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Button btnRechazar;
        private System.Windows.Forms.Button btnRevisionExperto;
        private System.Windows.Forms.Button btnVisualizarMapa;
        private System.Windows.Forms.Button btnSismograma;
        private System.Windows.Forms.TreeView treeAgrupados;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaOcurrencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn LatitudEpicentro;
        private System.Windows.Forms.DataGridViewTextBoxColumn LongitudEpicentro;
        private System.Windows.Forms.DataGridViewTextBoxColumn LatitudHipocentro;
        private System.Windows.Forms.DataGridViewTextBoxColumn LongitudHipocentro;
        private System.Windows.Forms.DataGridViewTextBoxColumn Magnitud;
        private System.Windows.Forms.Label lblEventos;
    }
}