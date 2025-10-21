using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using PPAI2025.Entidades;
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace PPAI2025.AccesoDatos
{
    public class AD_Usuario
    {
        public static List<Usuario> BuscarUsuarios()
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            List<Usuario> listaResultados = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM usuario";

                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                listaResultados = new List<Usuario>();

                foreach (DataRow fila in tabla.Rows)
                {
                    Usuario nuevoUsuario = new Usuario();

                    nuevoUsuario.NombreUsuario = fila["nombre_usuario"].ToString();
                    nuevoUsuario.Password = fila["password"].ToString();

                    string mailEmpleado= Convert.ToString(fila["mail_empleado"]);
                    nuevoUsuario.Empleado = ObtenerEmpleados(mailEmpleado);

                    listaResultados.Add(nuevoUsuario);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }

            return listaResultados;
        }

        public static Usuario ValidarCredenciales(string nombreUsuario, string password)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            Usuario listaResultados = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM usuario WHERE nombre_usuario = @nombreUsuario AND password = @password";

                cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    DataRow fila = tabla.Rows[0];
                    listaResultados = new Usuario();

                    listaResultados.NombreUsuario = fila["nombre_usuario"].ToString();
                    listaResultados.Password = fila["password"].ToString();

                    string mailEmpleado = fila["mail_empleado"].ToString();
                    listaResultados.Empleado = ObtenerEmpleados(mailEmpleado);
                }


            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }

            return listaResultados;

        }

        public static Empleado ObtenerEmpleados(string mailEmpleado)
        {
            return AD_Empleado.AgregarEmpleado(mailEmpleado);
        }
    }
}