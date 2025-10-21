using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.Entidades
{
    public class OrigenDeGeneracion
    {
        private string descripcion;
        private string nombre;

        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string Nombre { get => nombre; set => nombre = value; }

        public OrigenDeGeneracion(string descripcion, string nombre)
        {
            this.descripcion = descripcion;
            this.nombre = nombre;
        }

        public OrigenDeGeneracion()
        {
        }

        public String getNombre()
        {
            return Nombre.ToString();
        }
    }
}
