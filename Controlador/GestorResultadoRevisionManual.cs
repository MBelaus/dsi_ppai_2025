using PPAI2025.AccesoDatos;
using PPAI2025.DTOs;
using PPAI2025.Entidades;
using PPAI2025.Interfaces;
using PPAI2025.Interfaz;
using PPAI2025.Iteradores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPAI2025.Controlador
{
    public class GestorResultadoRevisionManual : IAgregado
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

        public List<EventoSismicoDTO> opResultadoRevisionManual()
        {
            cargarAtributos();
            (listESAD, listCambiosEstados) = buscaEventosAutodetectados();
            listESAD = ordenarPorFechaYHoraOcurrencia();

            List<EventoSismicoDTO> listEventoSismicoDTOs = new List<EventoSismicoDTO>();
            foreach(EventoSismico evento in listESAD)
            {
                listEventoSismicoDTOs.Add(MapearADTO(evento));
            }

            return listEventoSismicoDTOs;
        }

        private void cargarAtributos()
        {
            this.estados = AD_Estado.BuscarEstados();
            this.listES = AD_EventoSismico.BuscarTodosEventosSismicos();
            this.listSismografos = AD_Sismografo.BuscarTodosSismografos();
        }

        //private (List<EventoSismico>, List<CambioEstado> ) buscaEventosAutodetectados()
        //{
        //    List<EventoSismico> eventosAutodetectadosNoRevisados = new List<EventoSismico>();
        //    List<CambioEstado> ultimosCambiosEstados = new List<CambioEstado>();
        //    foreach (EventoSismico evento in listES)    
        //    {
        //        CambioEstado cambioEstadoNoRevisado = evento.esEventoNoRevisado();
        //        if (cambioEstadoNoRevisado != null)
        //        {
        //            ultimosCambiosEstados.Add(cambioEstadoNoRevisado);
        //            eventosAutodetectadosNoRevisados.Add(evento);
        //        }
        //    }

        //    List<EventoSismico> eventosAutodetectados = new List<EventoSismico>();
        //    foreach(EventoSismico evento in eventosAutodetectadosNoRevisados)
        //    {
        //        eventosAutodetectados.Add(evento.getDatos());
        //    }

        //    //MessageBox.Show("Cantidad de eventos Autodetectados no revisados " + eventosAutodetectadosNoRevisados.Count.ToString());
            
        //    return (eventosAutodetectados, ultimosCambiosEstados);
        //}
        private (List<EventoSismico>, List<CambioEstado>) buscaEventosAutodetectados()
        {
            List<EventoSismico> eventosFiltrados = new List<EventoSismico>();
            List<CambioEstado> ultimosCambiosEstados = new List<CambioEstado>();
            IIterador iteradorEventosSismicos = crearIterador(this.listES.Cast<object>().ToList());

            while (!iteradorEventosSismicos.haFinalizado())
            {

                EventoSismico eventoSismicoActual = (EventoSismico)iteradorEventosSismicos.elementoActual();
                eventosFiltrados.Add(eventoSismicoActual);

                CambioEstado cambioEstadoNoRevisado = eventoSismicoActual.esEventoNoRevisado();
                ultimosCambiosEstados.Add(cambioEstadoNoRevisado);

                iteradorEventosSismicos.siguiente();
            }

            return (eventosFiltrados, ultimosCambiosEstados);
        }

        public List<EventoSismico> ordenarPorFechaYHoraOcurrencia()
        {
            var eventosOrdenados = listESAD.OrderBy(e => e.FechaOcurrencia).ToList();

            return eventosOrdenados;
        }

        public void tomarSeleccionEventoPorIndice(int indiceSeleccionado)
        {
            if (indiceSeleccionado >= 0 && indiceSeleccionado < listESAD.Count)
            {
                EventoSismico eventoSeleccionado = listESAD[indiceSeleccionado];
                tomarSeleccionEventoIngresado(eventoSeleccionado);
            }
            else
            {
                MessageBox.Show("Índice de evento seleccionado fuera de rango.");
            }
        }

        public void tomarSeleccionEventoIngresado(EventoSismico eventoSeleccionado)
        {
            esSelec = eventoSeleccionado;
            esBloqRevi = buscarEstadoBloqueadoEnRevision();
            fechaHoraActual = getFechaHoraActual();
            Empleado responsableInspeccion = buscarUsuarioLogueado();
            //actualizarUltimoEstado(esSelec,listCambiosEstados,fechaHoraActual, esBloqRevi, responsableInspeccion);
            actualizarUltimoEstado(esSelec,fechaHoraActual, esBloqRevi, responsableInspeccion);
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
            actualizarUltimoEstado(esSelec, fechaHoraActual, esRechazado, responsableInspeccion);
            //FIN CU
            finCU();

        }
        public void tomarSeleccionAccionConfirmar()
        {

            
            fechaHoraActual = getFechaHoraActual();
            esConfirmado = buscarEstadoConfirmado();

            Empleado responsableInspeccion = buscarUsuarioLogueado();
            actualizarUltimoEstado(esSelec, fechaHoraActual, esConfirmado, responsableInspeccion);
            finCU();
            

        }
        public void tomarSeleccionAccionRevisionExperto()
        {
            fechaHoraActual = getFechaHoraActual();
            esRevisionExperto = buscarEstadoRevisionExperto();

            Empleado responsableInspeccion = buscarUsuarioLogueado();
            actualizarUltimoEstado(esSelec, fechaHoraActual, esRevisionExperto, responsableInspeccion);
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
        
        public void actualizarUltimoEstado(EventoSismico eventoSeleccionado, DateTime fechaHoraActual, Estado estadoAsignar, Empleado responsableInspeccion)
        {
            // MessageBox.Show("Usuario " + usuarioActual.ToString());
            CambioEstado ultimoCEDelEvento = eventoSeleccionado.esEventoNoRevisado();

            if (ultimoCEDelEvento != null)
                {
                    CambioEstado nuevoCambioEstado = eventoSeleccionado.actualizarUltimoEstado(fechaHoraActual, estadoAsignar, responsableInspeccion);
                    this.listCambiosEstados.Add(nuevoCambioEstado);

                    AD_CambioEstado.ActualizarCambioEstado(ultimoCEDelEvento.FechaHoraFin, eventoSeleccionado.FechaOcurrencia, eventoSeleccionado.FechaHoraFin, ultimoCEDelEvento.FechaHoraInicio);
                }
            else
            {
                CambioEstado cambioEstadoDelEvento = eventoSeleccionado.CambioEstado
                                                .FirstOrDefault(ce => ce.esEstadoActual());
                CambioEstado nuevoCambioEstado = eventoSeleccionado.actualizarUltimoEstado(fechaHoraActual, estadoAsignar, responsableInspeccion);

                AD_CambioEstado.ActualizarCambioEstado(cambioEstadoDelEvento.FechaHoraFin, eventoSeleccionado.FechaOcurrencia, eventoSeleccionado.FechaHoraFin, cambioEstadoDelEvento.FechaHoraInicio);
            }

            AD_CambioEstado.InsertarNuevoCambioEstado(eventoSeleccionado.FechaOcurrencia, eventoSeleccionado.FechaHoraFin, fechaHoraActual, estadoAsignar.Ambito, estadoAsignar.Nombre);

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

        private EventoSismicoDTO MapearADTO(EventoSismico evento)
        {
            return new EventoSismicoDTO(
                evento.FechaOcurrencia,
                evento.LatitudEpicentro,
                evento.LongitudEpicentro,
                evento.LatitudHipocentro,
                evento.LongitudHipocentro,
                evento.ValorMagnitud);
        }

        public void llamarCUGenerarSismograma()
        {

        }
        public void finCU()
        {
            MessageBox.Show("FIN CU");
            Application.Exit();
        }

        public IIterador crearIterador(List<object> elementos)
        {
            return new IteradorEventosSismicos(elementos);
        }
    }
}
