using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.Entidades
{
    public class Usuario
    {
        private string nombreUsuario;
        private string password;
        private Empleado empleado;
        public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
        public string Password { get => password; set => password = value; }
        public Empleado Empleado { get => empleado; set => empleado = value; }

        public Usuario()
        {
        }

        public Usuario(string nombreUsuario, string password, Empleado empleado)
        {
            this.nombreUsuario = nombreUsuario;
            this.password = password;
            this.empleado = empleado;
        }

        public string getASLogueado()
        {
            return $"Nombre de usuario: {NombreUsuario}";
        }

        public Empleado getRILogueado()
        {
            return this.empleado;
        }
    }
}
