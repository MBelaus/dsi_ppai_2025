using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.Entidades
{
    public class Sesion
    {
        private Usuario usuario;
        private DateTime fechaHora;

        public Usuario UsuarioActual { get => usuario; set => usuario = value; }
        public DateTime FechaHora { get => fechaHora; set => fechaHora = value; }

        public Sesion() 
        {
        }

        public Sesion(Usuario usuario)
        {
            this.usuario = usuario;
            this.fechaHora = DateTime.Now;
        }

        public void EstablecerUsuario(Usuario usuario)
        {
            this.usuario = usuario;
            this.fechaHora = DateTime.Now;
        }

        public Usuario conocerUsuario()
        {
            return usuario;
        }
    }
}
