using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PPAI2025.Entidades
{
    public class EventoSismico
    {
        private DateTime fechaHoraOcurrencia;
        private DateTime fechaHoraFin;
        private float latitudEpicentro;
        private float longitudEpicentro;
        private float latitudHipocentro;
        private float longitudHipocentro;
        private float valorMagnitud;
        private List<CambioEstado> cambioEstado;
        private List<SerieTemporal> serieTemporal;
        private ClasificacionSismo clasificacion;
        private MagnitudRichter magnitudRichter;
        private OrigenDeGeneracion origen;
        private AlcanceSismo alcance;

        public DateTime FechaOcurrencia { get => fechaHoraOcurrencia; set => fechaHoraOcurrencia = value; }
        public DateTime FechaHoraFin { get => fechaHoraFin; set => fechaHoraFin = value; }
        public float LatitudEpicentro { get => latitudEpicentro; set => latitudEpicentro = value; }
        public float LongitudEpicentro { get => longitudEpicentro; set => longitudEpicentro = value; }
        public float LatitudHipocentro { get => latitudHipocentro; set => latitudHipocentro = value; }
        public float LongitudHipocentro { get => longitudHipocentro; set => longitudHipocentro = value; }
        public float ValorMagnitud { get => valorMagnitud; set => valorMagnitud = value; }
        public List<CambioEstado> CambioEstado { get => cambioEstado; set => cambioEstado = value; }
        public List<SerieTemporal> SerieTemporal { get => serieTemporal; set => serieTemporal = value; }
        public ClasificacionSismo Clasificacion { get => clasificacion; set => clasificacion = value; }
        public MagnitudRichter Magnitud { get => magnitudRichter; set => magnitudRichter = value; }
        public OrigenDeGeneracion Origen { get => origen; set => origen = value; }
        public AlcanceSismo Alcance { get => alcance; set => alcance = value; }

        public EventoSismico(DateTime fechaHoraOcurrencia, DateTime fechaHoraFin, float latitudEpicentro, float longitudEpicentro, float latitudHipocentro, float longitudHipocentro, float valorMagnitud,
            List<CambioEstado> cambioEstado, List<SerieTemporal> serieTemporal, ClasificacionSismo clasificacion, MagnitudRichter magnitudRichter, OrigenDeGeneracion origen, AlcanceSismo alcance)
        {
            this.fechaHoraOcurrencia = fechaHoraOcurrencia;
            this.fechaHoraFin = fechaHoraFin;
            this.latitudEpicentro = latitudEpicentro;
            this.longitudEpicentro = longitudEpicentro;
            this.latitudHipocentro = latitudHipocentro;
            this.longitudHipocentro = longitudHipocentro;
            this.valorMagnitud = valorMagnitud;
            this.cambioEstado = cambioEstado;
            this.serieTemporal = serieTemporal;
            this.clasificacion = clasificacion;
            this.magnitudRichter = magnitudRichter;
            this.origen = origen;
            this.alcance = alcance;
        }

        public EventoSismico()
        {
        }

        public CambioEstado esEventoNoRevisado()
        {
            CambioEstado cambioEstado = this.CambioEstado
                                            .FirstOrDefault(ce => ce.esAutoDetectado());
            if (cambioEstado != null) { 
                CambioEstado cambioEstadoActual = this.CambioEstado
                                                .FirstOrDefault(ce => ce.esEstadoActual());

                if (cambioEstadoActual == null)
                {
                    return null;
                }
                else
                {
                    if (cambioEstadoActual.esNoRevisado())
                    {
                        return cambioEstadoActual;
                    }
                    return null;
                }
            }
            return null;
        }

        public EventoSismico getDatos()
        {
            this.Magnitud.getNumero();
            return this;
        }


        public void actualizarUltimoEstado(List<CambioEstado> listUltimos, DateTime fechaHoraActual, Estado estado, Empleado responsableInspeccion)
        {

            //MessageBox.Show("Cantidad cambios estado: " + this.CambioEstado.Count.ToString());

            CambioEstado cambioEstadoDelEvento = listUltimos
                    .FirstOrDefault(ce => this.CambioEstado.Contains(ce));

            if (cambioEstadoDelEvento != null)
            {
                cambioEstadoDelEvento.setFechaHoraFin(fechaHoraActual);
            }
            crearNuevoCambioEstado(estado,responsableInspeccion);
        }

        private void crearNuevoCambioEstado(Estado est, Empleado responsableInspeccion)
        {
            CambioEstado nuevoCambio = new CambioEstado
            {
                FechaHoraFin = null,
                FechaHoraInicio = DateTime.Now,
                EstadoActual = est,
                ResponsableInspeccion = responsableInspeccion
            };

            MessageBox.Show("Nuevo cambio estado: " + nuevoCambio.EstadoActual.Nombre.ToString());
            this.CambioEstado.Add(nuevoCambio);
        }

        public (string, string, string) buscarDatosSismo()
        {

            var nombreAlcance = this.Alcance.getNombre();
            var nombreClasificacion = this.Clasificacion.getNombre();
            var nombreOrigen = this.Origen.getNombre();

            return (nombreAlcance, nombreClasificacion, nombreOrigen);
        }

        public object buscarYClasificarSeriesTemporales(List<Sismografo> sismografos)
        {
            var serieToStationMap = sismografos
            .SelectMany(sismo => sismo.SeriesTemporales.Select(serie => new { Serie = serie, Station = sismo.EstacionSismologica }))
            .ToDictionary(x => x.Serie, x => x.Station);

            var gruposPorEstacion = this.SerieTemporal
            .Where(s => serieToStationMap.ContainsKey(s) && serieToStationMap[s] != null)
            .GroupBy(s => serieToStationMap[s].Nombre)
            .Select(g => new
        {
                nombreEstacion = g.Key,
                seriesDeEstaEstacion = g.Select(s => s.getDatos()).ToList()
        })
        .ToList();

            //var gruposPorEstacion = this.SerieTemporal
            //.Where(s => s.buscarEstacionSismologica() != null)
            //.GroupBy(s => s.buscarEstacionSismologica().Nombre)
            //.Select(g => new
            //{
            //    nombreEstacion = g.Key,
            //    seriesDeEstaEstacion = g.Select(s => s.getDatos()).ToList()
            //})
            //.ToList();

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string jsonOutput = JsonSerializer.Serialize(gruposPorEstacion, options);


            return gruposPorEstacion;
        }
    }
    
}
