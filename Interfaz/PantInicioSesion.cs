using PPAI2025.AccesoDatos;
using PPAI2025.Controlador;
using PPAI2025.Entidades;
using PPAI2025.Interfaz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PPAI2025
{
    public partial class PantInicioSesion : Form
    {
        private GestorInicioSesion gestorInicioSesion;
        public PantInicioSesion()
        {
            InitializeComponent();
            this.gestorInicioSesion = new GestorInicioSesion(this);
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtNombreUsuario.Text; 
            string password = txtPassword.Text;
            gestorInicioSesion.AutenticarUsuario(nombreUsuario, password);
        }

        public void InicioSesionExitoso(Sesion sesionActual)
        {
            PantResultadoRevisionManual menuPrincipal = new PantResultadoRevisionManual();
            this.Hide();
            menuPrincipal.regResultRevisionManual(sesionActual);
        }

        public void InicioSesionFallido()
        {
            MessageBox.Show("Credenciales incorrectas, verifique los datos ingresados", "Error de Login",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Login1_Load(object sender, EventArgs e)
        {

        }
    }
}
