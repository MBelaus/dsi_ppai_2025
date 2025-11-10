using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.Entidades
{
    public class Estado
    {
        private string nombreEstado;
        private string ambito;
        private bool esEstadoCancelado;
        private bool esEstadoRechazado;
        private bool esEstadoBloqueadoRevision;

        public string Nombre { get => nombreEstado; set => nombreEstado = value; }
        public string Ambito { get => ambito; set => ambito = value; }
        public bool EsCancelado{ get => esEstadoCancelado; set => esEstadoCancelado = value; }
        public bool EsRechazado { get => esEstadoRechazado; set => esEstadoRechazado = value; }
        public bool EsBloqueadoEnRevision { get => esEstadoBloqueadoRevision; set => esEstadoBloqueadoRevision = value; }

        public Estado(string nombre, string ambito)///, bool esReservable, bool esCancelable, bool esBloqueadoEnRevision)
        {
            this.nombreEstado = nombre;
            this.ambito = ambito;
            //this.esEstadoCancelado = esReservable;
            //this.esEstadoRechazado = esCancelable;
            //this.esEstadoBloqueadoRevision = esBloqueadoEnRevision;
        }

        public Estado()
        {
        }

        public bool esAmbitoEventoSismico()
        {
            return this.Ambito.Equals("EventoSismico", StringComparison.OrdinalIgnoreCase);
        }

        public bool esNoRevisado()
        {
            return this.Nombre.Equals("no revisado", StringComparison.OrdinalIgnoreCase);
        }

        public bool esBloqueadoEnRevision()
        {
            return this.Nombre.Equals("bloqueado en revision", StringComparison.OrdinalIgnoreCase);
        }

        public bool esRechazado()
        {
            return this.Nombre.Equals("rechazado", StringComparison.OrdinalIgnoreCase);
        }
        public bool esRevisionExperto()
        {
            return this.Nombre.Equals("revision experto", StringComparison.OrdinalIgnoreCase);
        }
        public bool esConfirmado()
        {
            return this.Nombre.Equals("confirmado", StringComparison.OrdinalIgnoreCase);
        }

        public bool esAutoDetectado()
        {
            return this.Nombre.Equals("auto detectado", StringComparison.OrdinalIgnoreCase);
        }
    }
}
