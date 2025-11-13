using PPAI2025.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.Interfaces
{
    public interface IIterador
    {
        void primero();
        void siguiente();
        bool cumpleFiltro();
        bool haFinalizado();
        EventoSismico elementoActual();
    }
}
