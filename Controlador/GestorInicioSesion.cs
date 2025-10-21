using PPAI2025.AccesoDatos;
using PPAI2025.Entidades;
using PPAI2025.Interfaz;
using System;

namespace PPAI2025.Controlador
{
    public class GestorInicioSesion
    {
        private PantInicioSesion pantalla;
        private Sesion sesionActual;

        public GestorInicioSesion(PantInicioSesion pantalla)
        {
            this.pantalla = pantalla;
            this.sesionActual = new Sesion();
        }

        public void AutenticarUsuario(string nombreUsuario, string password)
        {
            Usuario usuario = AD_Usuario.ValidarCredenciales(nombreUsuario, password);

            if (usuario != null)
            {
                sesionActual.EstablecerUsuario(usuario);
                pantalla.InicioSesionExitoso(this.sesionActual);
            }
            else
            {
                pantalla.InicioSesionFallido();
            }
        }
    }
}