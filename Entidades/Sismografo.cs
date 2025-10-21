using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.Entidades
{
    public class Sismografo
    {
        private int idSismografo;
        private DateTime fechaAdquisicion;
        private string nroSerie;
        private EstacionSismologica estacionSismologica;
        private List<SerieTemporal> seriesTemporales;

        public int Id { get => idSismografo; set => idSismografo = value; }
        public DateTime FechaAdquisicion { get => fechaAdquisicion; set => fechaAdquisicion = value; }
        public string NroSerie { get => nroSerie; set => nroSerie = value; }
        public EstacionSismologica EstacionSismologica { get => estacionSismologica; set => estacionSismologica = value; }
        public List<SerieTemporal> SeriesTemporales { get => seriesTemporales; set => seriesTemporales = value; }

        public Sismografo(int idSismografo, DateTime fechaAdquisicion, string nroSerie, EstacionSismologica estacionSismologica, List<SerieTemporal> seriesTemporales)
        {
            this.idSismografo = idSismografo;
            this.fechaAdquisicion = fechaAdquisicion;
            this.nroSerie = nroSerie;
            this.estacionSismologica = estacionSismologica;
            this.seriesTemporales = seriesTemporales;
        }

        public Sismografo()
        {
        }

        public EstacionSismologica getEstacionSismologica()
        {
            return this.EstacionSismologica.getNombre();
        }

        public EstacionSismologica buscarEstacionSismologica()
        {
            return this.EstacionSismologica;
        }



    }
}
