using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.Entidades
{
    public class MuestraSismica
    {
        private DateTime fechaHoraMuestra;
        private List<DetalleMuestraSismica> detalleMuestraSismica;

        public DateTime FechaHoraMuestra { get => fechaHoraMuestra; set => fechaHoraMuestra = value; }
        public List<DetalleMuestraSismica> DetalleMuestraSismica { get => detalleMuestraSismica; set => detalleMuestraSismica = value; }

        public MuestraSismica(DateTime fechaHoraMuestra, List<DetalleMuestraSismica> detalleMuestraSismica)
        {
            this.fechaHoraMuestra = fechaHoraMuestra;
            this.detalleMuestraSismica = detalleMuestraSismica;
        }

        public MuestraSismica()
        {
        }

        public object getDatos()
        {
            return new
            {
                fechaMuestra = this.FechaHoraMuestra.ToString("yyyy-MM-ddTHH:mm:ss.fff"),
                detalles = this.DetalleMuestraSismica?.Select(d => d.getDatos()).ToList()
            };
        }
        /*
        public MuestraSismicaInfo getDatos() // Renamed to follow naming conventions
        {
            var muestraInfo = new MuestraSismicaInfo
            {
                FechaHoraMuestra = FechaHoraMuestra, 
                DetallesMuestraSismica = ObtenerDetallesDeMuestra()
            };
            return muestraInfo; // Ensure a value is returned
        }
        */
    }
}

