using PPAI2025.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.AccesoDatos
{
    public static class AppSession
    {
        public static Sesion SesionActual { get; set; }

        public static Usuario GetUsuarioLogueado()
        {
            if (SesionActual != null)
            {
                return SesionActual.conocerUsuario();
            }
            return null;
        }
    }
}
