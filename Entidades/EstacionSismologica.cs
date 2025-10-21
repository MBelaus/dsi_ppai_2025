using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.Entidades
{
    public class EstacionSismologica
    {
        private int codigoEstacion;
        private string documentoCertificacionAdq;
        private DateTime fechaSolicitudCertificacion;
        private float longitud;
        private float latitud;
        private string nombre;
        private int numeroCertificadoAdquisicion;

        public int CodigoEstacion { get => codigoEstacion; set => codigoEstacion = value; }
        public string DocumentoCertificacionAdq { get => documentoCertificacionAdq; set => documentoCertificacionAdq = value; }
        public DateTime FechaSolicitudCertificacion { get => fechaSolicitudCertificacion; set => fechaSolicitudCertificacion = value; }
        public float Longitud { get => longitud; set => longitud = value;}
        public float Latitud { get => latitud; set => latitud = value;}
        public string Nombre { get => nombre; set => nombre = value; }
        public int NumeroCertificadoAdquisicion { get => numeroCertificadoAdquisicion; set => numeroCertificadoAdquisicion = value; }

        public EstacionSismologica(int codigoEstacion, string documentoCertificacionAdq, DateTime fechaSolicitudCertificacion, float longitud, float latitud, string nombre, int numeroCertificadoAdquisicion)
        {
            this.codigoEstacion = codigoEstacion;
            this.documentoCertificacionAdq = documentoCertificacionAdq;
            this.fechaSolicitudCertificacion = fechaSolicitudCertificacion;
            this.longitud = longitud;
            this.latitud = latitud;
            this.nombre = nombre;
            this.numeroCertificadoAdquisicion = numeroCertificadoAdquisicion;
        }

        public EstacionSismologica()
        {
        }

        public EstacionSismologica getNombre()
        {
            return this;
        }
    }
}
