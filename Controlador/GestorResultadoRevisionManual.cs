using PPAI2025.AccesoDatos;
using PPAI2025.Entidades;
using PPAI2025.Interfaz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPAI2025.Controlador
{
    public class GestorResultadoRevisionManual
    {
        private PantResultadoRevisionManual pantalla;
        private List<EventoSismico> listES;
        private List<EventoSismico> listESAD;
        private List<Sismografo> listSismografos;
        private EventoSismico esSelec;
        private Estado esBloqRevi;
        private Estado esRechazado;
        private Estado esConfirmado;
        private Estado esRevisionExperto;
        private DateTime fechaHoraActual;
        private dynamic seriesTemporales;
        private Sesion sesionActual;
        private List<Estado> estados;
        private Estado estadoEnRevision;
        private List<CambioEstado> listCambiosEstados;

        public GestorResultadoRevisionManual(PantResultadoRevisionManual pantalla, Sesion sesion) 
        {
            this.pantalla = pantalla;
            this.sesionActual = sesion;
        }

        public List<EventoSismico> opResultadoRevisionManual()
        {
            cargarAtributos();
            (listESAD, listCambiosEstados) = buscaEventosAutodetectados();
            listESAD = ordenarPorFechaYHoraOcurrencia();
            return listESAD;
        }

        private void cargarAtributos()
        {
            this.estados = AD_Estado.BuscarEstados();
            this.listES = AD_EventoSismico.BuscarTodosEventosSismicos();
            this.listSismografos = AD_Sismografo.BuscarTodosSismografos();
        }

        private (List<EventoSismico>, List<CambioEstado> ) buscaEventosAutodetectados()
        {
            List<EventoSismico> eventosAutodetectadosNoRevisados = new List<EventoSismico>();
            List<CambioEstado> ultimosCambiosEstados = new List<CambioEstado>();
            foreach (EventoSismico evento in listES)    
            {

                if (evento.esEventoNoRevisado() != null)
                {
                    ultimosCambiosEstados.Add(evento.esEventoNoRevisado());
                    eventosAutodetectadosNoRevisados.Add(evento);
                }
            }

            List<EventoSismico> eventosAutodetectados = new List<EventoSismico>();
            foreach(EventoSismico evento in eventosAutodetectadosNoRevisados)
            {
                eventosAutodetectados.Add(evento.getDatos());
            }

            //MessageBox.Show("Cantidad de eventos Autodetectados no revisados " + eventosAutodetectadosNoRevisados.Count.ToString());
            
            return (eventosAutodetectados, ultimosCambiosEstados);
        }

        public List<EventoSismico> ordenarPorFechaYHoraOcurrencia()
        {
            var eventosOrdenados = listESAD.OrderBy(e => e.FechaOcurrencia).ToList();

            return eventosOrdenados;
        }

        public void tomarSeleccionEventoIngresado(EventoSismico eventoSeleccionado)
        {
            esSelec = eventoSeleccionado;
            esBloqRevi = buscarEstadoBloqueadoEnRevision();
            fechaHoraActual = getFechaHoraActual();
            Empleado responsableInspeccion = buscarUsuarioLogueado();
            actualizarUltimoEstado(esSelec,listCambiosEstados,fechaHoraActual, esBloqRevi, responsableInspeccion);
            buscarDatosSismicosEventoSeleccionado(esSelec);
            habilitarOpcionVisualizarMapa();
            permitirModificacionDatos();
            pantalla.solicitarOpcionVisualizarMapa();
        }

        public void habilitarOpcionVisualizarMapa()
        {
            pantalla.solicitarOpcionVisualizarMapa();
        }

        public void tomarOpcionVisualizarMapaIngresada()
        {
            MessageBox.Show("No implementada");
        }

        public void permitirModificacionDatos()
        {
            pantalla.solicitarModificacionDatos();
        }
        public void tomarModificacionDatosIngresada()
        {
            solicitarSeleccionAccion();
        }
        public void solicitarSeleccionAccion()
        {
            pantalla.solicitarSeleccionAccion();
        }


        public void tomarSeleccionAccionIngresada()
        {

            //FALTA EL VALIDAR DATOS
            fechaHoraActual = getFechaHoraActual();
            esRechazado = buscarEstadoRechazado();
            
            Empleado responsableInspeccion = buscarUsuarioLogueado();
            actualizarUltimoEstado(esSelec, listCambiosEstados, fechaHoraActual, esRechazado, responsableInspeccion);
            //FIN CU
            finCU();

        }
        public void tomarSeleccionAccionConfirmar()
        {

            
            fechaHoraActual = getFechaHoraActual();
            esConfirmado = buscarEstadoConfirmado();

            Empleado responsableInspeccion = buscarUsuarioLogueado();
            actualizarUltimoEstado(esSelec, listCambiosEstados, fechaHoraActual, esConfirmado, responsableInspeccion);
            finCU();
            

        }
        public void tomarSeleccionAccionRevisionExperto()
        {
            fechaHoraActual = getFechaHoraActual();
            esRevisionExperto = buscarEstadoRevisionExperto();

            Empleado responsableInspeccion = buscarUsuarioLogueado();
            actualizarUltimoEstado(esSelec, listCambiosEstados, fechaHoraActual, esRevisionExperto, responsableInspeccion);
            finCU();

        }


        public Estado buscarEstadoBloqueadoEnRevision()
        {
            foreach(Estado e in estados)
            {
                if(e.esAmbitoEventoSismico() && e.esBloqueadoEnRevision())
                {
                    return e;
                }
            }

            return null;
        }

        public DateTime getFechaHoraActual()
        {
            return DateTime.Now;
        }
        
        public Empleado buscarUsuarioLogueado()
        {
            Usuario usuarioLogueado = sesionActual.conocerUsuario();
            Empleado responsableInspeccion = usuarioLogueado.getRILogueado();
            return responsableInspeccion;

        }
        
        public void actualizarUltimoEstado(EventoSismico eventoSeleccionado,List<CambioEstado> listUltimos, DateTime fechaHoraActual, Estado estadoAsignar, Empleado responsableInspeccion)
        {
            // MessageBox.Show("Usuario " + usuarioActual.ToString());
            CambioEstado ultimoCEDelEvento = eventoSeleccionado.esEventoNoRevisado();

            if(ultimoCEDelEvento != null)
                {
                    eventoSeleccionado.actualizarUltimoEstado(listUltimos, fechaHoraActual, estadoAsignar, responsableInspeccion);
                }

        }

        private void buscarDatosSismicosEventoSeleccionado(EventoSismico eventoSeleccionado)
        {
            (string alcance, string clasificacion, string origen) = eventoSeleccionado.buscarDatosSismo();
            //this.seriesTemporales = eventoSeleccionado.buscarSeriesTemporales();
            this.seriesTemporales = eventoSeleccionado.buscarYClasificarSeriesTemporales(listSismografos);
            this.pantalla.CargarDatosEnTreeView(this.seriesTemporales, alcance, clasificacion, origen);
            llamarCUGenerarSismograma();
            habilitarOpcionVisualizarMapa();
        }

        public Estado buscarEstadoRechazado()
        {
            foreach (Estado e in estados)
            {
                if (e.esAmbitoEventoSismico() && e.esRechazado())
                {
                    return e;
                }
            }

            return null;
        }
        public Estado buscarEstadoRevisionExperto()
        {
            foreach (Estado e in estados)
            {
                if (e.esAmbitoEventoSismico() && e.esRevisionExperto())
                {
                    return e;
                }
            }

            return null;
        }
        public Estado buscarEstadoConfirmado()
        {
            foreach (Estado e in estados)
            {
                if (e.esAmbitoEventoSismico() && e.esConfirmado())
                {
                    return e;
                }
            }

            return null;
        }

        public void llamarCUGenerarSismograma()
        {

        }
        public void finCU()
        {
            MessageBox.Show("FIN CU");
            Application.Exit();
        }
    }
}
