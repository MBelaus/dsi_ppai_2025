using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.Entidades
{
    public class AlcanceSismo
    {
        private string descripcion;
        private string nombre;

        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string Nombre { get => nombre; set => nombre = value; }

        public AlcanceSismo(string descripcion, string nombre)
        {
            this.descripcion = descripcion;
            this.nombre = nombre;
        }

        public AlcanceSismo()
        {
        }

        public string getNombre()
        {
            return Nombre.ToString();
        }
    }
}
