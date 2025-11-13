using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PPAI2025.Controlador;
using PPAI2025.DTOs;
using PPAI2025.Entidades;

namespace PPAI2025.Interfaz
{

    public partial class PantResultadoRevisionManual : Form
    {
        private GestorResultadoRevisionManual gestor;
        private bool datosCargados = false;

        public PantResultadoRevisionManual(GestorResultadoRevisionManual gestor)
        {
            InitializeComponent();
            this.gestor = gestor;

        }
        public PantResultadoRevisionManual()
        {
        }

        public void Principal_Load(object sender, EventArgs e)
        {
            
        }

        public void regResultRevisionManual(Sesion sesionActual)
        {
            habilitarPantalla(sesionActual);
        }

        public void habilitarPantalla(Sesion sesionActual)
        {
            gestor = new GestorResultadoRevisionManual(this, sesionActual);
            PantResultadoRevisionManual ventana = new PantResultadoRevisionManual(gestor);
            List<EventoSismicoDTO> listaEventosDTO = gestor.opResultadoRevisionManual();
            ventana.mostrarDatosEventos(listaEventosDTO);
            ventana.Show();
            solicitarSeleccionEvento();
        }

        public void solicitarSeleccionEvento()
        {
            MessageBox.Show("Seleccione un Evento de la grilla");
        }

        public void mostrarDatosEventos(List<EventoSismicoDTO> eventosDTO)
        {
            dataGridEventos.DataSource = null;
            dataGridEventos.DataSource = eventosDTO;

            dataGridEventos.Columns["FechaOcurrencia"].HeaderText = "Fecha de Ocurrencia";
            dataGridEventos.Columns["LatitudEpicentro"].HeaderText = "Latitud Epicentro";
            dataGridEventos.Columns["LatitudHipocentro"].HeaderText = "Latitud Hipocentro";
            dataGridEventos.Columns["LongitudEpicentro"].HeaderText = "Longitud Epicentro";
            dataGridEventos.Columns["LongitudHipocentro"].HeaderText = "Longitud Hipocentro";

            dataGridEventos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridEventos.MultiSelect = false;

            datosCargados = true;

        }

        private void tomarSeleccionEvento_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if(e.RowIndex < 0) return;

            int index = e.RowIndex;

            gestor.tomarSeleccionEventoPorIndice(e.RowIndex);

            //DataGridViewRow filaSeleccionada = dataGridEventos.Rows[index];

            //EventoSismico eventoSeleccionado = filaSeleccionada.DataBoundItem as EventoSismico;
            //if (eventoSeleccionado != null)
            //{
            //    gestor.tomarSeleccionEventoIngresado(eventoSeleccionado);
            //}

            btnSismograma.Enabled = true;
            btnConfirmar.Enabled = true;
            btnRechazar.Enabled = true;
            btnRevisionExperto.Enabled = true;
            btnVisualizarMapa.Enabled = true;
            dataGridEventos.Enabled = false;

        }

        public void solicitarOpcionVisualizarMapa()
        {
        }

        public void tomarIngresoOpcVisualizarMapa()
        {
            gestor.tomarOpcionVisualizarMapaIngresada();
        }

        public void solicitarModificacionDatos()
        {
            DialogResult dialogResult = MessageBox.Show("Desea modificar datos?", "Modificar datos", MessageBoxButtons.YesNo);
            tomarIngresoModificacionDatos();

        }


        public void tomarIngresoModificacionDatos()
        {
            gestor.tomarModificacionDatosIngresada();
        }

        public void solicitarSeleccionAccion()
        {
            MessageBox.Show("Seleccione una opcion a realizar para el evento");
            
            //btnConfirmar.Enabled = true;
            //btnRechazar.Enabled = true;
            //btnRevisionExperto.Enabled = true;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            gestor.tomarSeleccionAccionConfirmar();
        }

        private void btnRevisionExperto_Click(object sender, EventArgs e)
        {
            gestor.tomarSeleccionAccionRevisionExperto();
        }

        private void tomarIngresoSeleccionAccion_Click(object sender, EventArgs e)
        {
            gestor.tomarSeleccionAccionIngresada();
        }

        private void solicitarOpcionVisualizarMapa_Click(object sender, EventArgs e)
        {
            tomarIngresoOpcVisualizarMapa();
        }


        public void CargarDatosEnTreeView(dynamic gruposPorEstacion, string alcance, string clasificacion, string origen)
        {
            if (treeAgrupados == null)
            {
                treeAgrupados = new System.Windows.Forms.TreeView();
                treeAgrupados.Location = new System.Drawing.Point(12, 12);
                treeAgrupados.Size = new System.Drawing.Size(400, 300);
                this.Controls.Add(treeAgrupados);
            }
            
            
            treeAgrupados.Nodes.Clear();

            var datosSismoNode = new System.Windows.Forms.TreeNode($"Datos Sismo:");
            treeAgrupados.Nodes.Add(datosSismoNode);
            datosSismoNode.Nodes.Add($"Alcance: {alcance}");
            datosSismoNode.Nodes.Add($"Clasificación: {clasificacion}");
            datosSismoNode.Nodes.Add($"Origen: {origen}");

            datosSismoNode.Expand();

            foreach (var grupoEstacion in gruposPorEstacion)
            {
                var estacionNode = new System.Windows.Forms.TreeNode(grupoEstacion.nombreEstacion);

                foreach (var serie in grupoEstacion.seriesDeEstaEstacion)
                {
                    if (grupoEstacion.seriesDeEstaEstacion != null)
                    {
                        var serieNode = new System.Windows.Forms.TreeNode("Serie:");

                        serieNode.Nodes.Add($"Fecha/Hora registro: {serie.fechaHoraInicioMuestras}");
                        serieNode.Nodes.Add($"Frecuencia de muestreo: {serie.frecuenciaMuestras}");
                        serieNode.Nodes.Add($"Alerta de alarma: {serie.condicionAlarma}");

                        serieNode.Expand();

                        foreach (var muestra in serie.muestras)
                        {
                            
                            var muestraNode = new System.Windows.Forms.TreeNode($"Fecha/Hora muestra: {muestra.fechaMuestra}");
                            muestraNode.Expand();

                            foreach (var detalle in muestra.detalles)
                            {
                                var detalleNode = new System.Windows.Forms.TreeNode($"{detalle.unidadMedida}: {detalle.valor} {detalle.tipoDato}");
                                muestraNode.Nodes.Add(detalleNode);
                            }

                            serieNode.Nodes.Add(muestraNode);
                        }
                        estacionNode.Nodes.Add(serieNode);
                    }
                }

                treeAgrupados.Nodes.Add(estacionNode);
            }

            treeAgrupados.ExpandAll();
            
            treeAgrupados.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            treeAgrupados.Size = new System.Drawing.Size(this.ClientSize.Width - 10, this.ClientSize.Height - 10);
            this.Size = new System.Drawing.Size(550, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowDialog();

        }

        private void treeAgrupados_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void btnSismograma_Click(object sender, EventArgs e)
        {
            MessageBox.Show("No implementada");
        }

        private void dataGridEventos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridEventos.ClearSelection();
        }
    }




}
