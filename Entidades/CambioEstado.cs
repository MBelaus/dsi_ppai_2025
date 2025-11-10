using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPAI2025.Entidades
{
    public class CambioEstado
    {
        private DateTime? fechaHoraFin;
        private DateTime? fechaHoraInicio;
        private Estado estado;
        private Empleado responsableInspeccion;

        public DateTime? FechaHoraFin { get => fechaHoraFin; set => fechaHoraFin = value; }
        public DateTime? FechaHoraInicio { get => fechaHoraInicio; set => fechaHoraInicio = value; }
        public Estado EstadoActual { get => estado; set => estado = value; }
        public Empleado ResponsableInspeccion { get => responsableInspeccion; set => responsableInspeccion = value; }

        public CambioEstado(DateTime? fechaHoraInicio, DateTime? fechaHoraFin, Estado estado, Empleado responsableInspeccion)
        {
            this.fechaHoraInicio = fechaHoraInicio;
            this.estado = estado;
            this.fechaHoraFin = fechaHoraFin;
            this.responsableInspeccion = responsableInspeccion;
        }

        public CambioEstado()
        {
        }

        public bool esEstadoActual()
        {
            if (fechaHoraFin == null)
            {
                return true;
            }
            return false;
        }

        public bool esNoRevisado()
        {
            bool estadoEsAmbitoEventoSismico = this.EstadoActual.esAmbitoEventoSismico();
            bool estadoEsNoRevisado = this.EstadoActual.esNoRevisado();

            return estadoEsNoRevisado && estadoEsAmbitoEventoSismico;
        }

        public void setFechaHoraFin(DateTime fechaHoraActual)
        {
            this.FechaHoraFin = fechaHoraActual;
        }

        public bool esAutoDetectado()
        {
            bool estadoEsAmbitoEventoSismico = this.EstadoActual.esAmbitoEventoSismico();
            bool estadoAutoDetectado = this.EstadoActual.esAutoDetectado();

            return estadoAutoDetectado && estadoEsAmbitoEventoSismico;
        }
    }
}
