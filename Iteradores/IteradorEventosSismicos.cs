using PPAI2025.Entidades;
using PPAI2025.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.Iteradores
{
    public class IteradorEventosSismicos : IIterador
    {
    private int posicion;
    private List<EventoSismico> listaEventosSismicos;

        public IteradorEventosSismicos(List<object> listaGenerica)
        {
            this.listaEventosSismicos = listaGenerica.OfType<EventoSismico>().ToList();
            primero();
        }
        public bool cumpleFiltro()
        {
            EventoSismico eventoActual = (EventoSismico)elementoActual();
            CambioEstado cambioEstadoNoRevisado = eventoActual.esEventoNoRevisado();

            return cambioEstadoNoRevisado != null;
        }
        public object elementoActual()
        {
            return this.listaEventosSismicos[this.posicion];
        }
        public bool haFinalizado()
        {
            return this.posicion >= this.listaEventosSismicos.Count;
        }
        public void primero()
        {
            this.posicion = 0;
            while (!haFinalizado() && !cumpleFiltro())
            {
                this.posicion++;
            }
        }
        public void siguiente()
        {
            this.posicion++;
            while (!haFinalizado() && !cumpleFiltro())
            {
                this.posicion++;
            }
        }
    }
}
