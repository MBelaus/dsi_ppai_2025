using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.Entidades
{
    public class SerieTemporal
    {
        private string condicionAlarma;
        private DateTime fechaHoraRegistro;
        private DateTime fechaHoraInicioMuestras;
	    private string frecuenciaMuestreo;
        private List<MuestraSismica> muestras;

        public string CondicionAlarma { get => condicionAlarma; set => condicionAlarma = value; }
        public DateTime FechaHoraRegistro { get => fechaHoraRegistro; set => fechaHoraRegistro = value; }
        public DateTime FechaHoraInicioRegistroMuestras { get => fechaHoraInicioMuestras; set => fechaHoraInicioMuestras = value; }
        public string FrecuenciaMuestreo { get => frecuenciaMuestreo; set => frecuenciaMuestreo = value; }
        public List<MuestraSismica> Muestras { get => muestras; set => muestras = value; }

        public SerieTemporal(string condicionAlarma, DateTime fechaHoraRegistro, DateTime fechaHoraInicioMuestras, string frecuenciaMuestreo, List<MuestraSismica> muestras)
        { 
            this.condicionAlarma = condicionAlarma;
            this.fechaHoraRegistro = fechaHoraRegistro;
            this.fechaHoraInicioMuestras = fechaHoraInicioMuestras;
            this.frecuenciaMuestreo = frecuenciaMuestreo;
            this.muestras = muestras;
        }

        public SerieTemporal()
        {
        }

        public object getDatos()
        {
            return new
            {
                condicionAlarma = this.CondicionAlarma,
                fechaHoraRegistro = this.FechaHoraRegistro.ToString("yyyy-MM-ddTHH:mm:ss"),
                fechaHoraInicioMuestras = this.FechaHoraInicioRegistroMuestras.ToString("yyyy-MM-ddTHH:mm:ss"),
                frecuenciaMuestras = this.FrecuenciaMuestreo,
                muestras = this.Muestras?.Select(m => m.getDatos()).ToList()
            };
        }
        //public EstacionSismologica buscarEstacionSismologica()
        //{
        //    return this.Sismografo?.getEstacionSismologica();
        //}
    }
}
