using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.DTOs
{
    public class EventoSismicoDTO
    {
        public DateTime FechaOcurrencia { get; set; }
        public float LatitudEpicentro { get; set;  }
        public float LongitudEpicentro { get; set; }
        public float LatitudHipocentro { get; set; }
        public float LongitudHipocentro { get; set;  }
        public float ValorMagnitud { get; set; }


        public EventoSismicoDTO(DateTime fechaOcurrencia, float latitudEpicentro, float longitudEpicentro, float latitudHipocentro, float longitudHipocentro, float valorMagnitud)
        {
            FechaOcurrencia = fechaOcurrencia;
            LatitudEpicentro = latitudEpicentro;
            LongitudEpicentro = longitudEpicentro;
            LatitudHipocentro = latitudHipocentro;
            LongitudHipocentro = longitudHipocentro;
            ValorMagnitud = valorMagnitud;
        }
    }
}
