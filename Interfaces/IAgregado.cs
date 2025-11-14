using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.Interfaces
{
    public interface IAgregado
    {
        IIterador crearIterador(List<object> elementos);
    }
}
