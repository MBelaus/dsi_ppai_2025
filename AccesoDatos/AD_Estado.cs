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
    public class AD_Estado
    {
        public static List<Estado> BuscarEstados()
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            List<Estado> listaResultados = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM estado";

                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                listaResultados = new List<Estado>();

                foreach (DataRow fila in tabla.Rows)
                {
                    Estado nuevoEstado = new Estado
                    {
                        Nombre = fila["nombre"].ToString(),
                        Ambito = fila["ambito"].ToString()
                    };

                    listaResultados.Add(nuevoEstado); 
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

        public static Estado AgregarEstado(string ambitoEstado, string nombreEstado)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            Estado listaResultados = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM estado WHERE ambito = @ambitoEstado AND nombre = @nombreEstado";

                cmd.Parameters.AddWithValue("@ambitoEstado", ambitoEstado);
                cmd.Parameters.AddWithValue("@nombreEstado", nombreEstado);
                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    DataRow fila = tabla.Rows[0];

                    listaResultados = new Estado
                    {
                        Nombre = fila["nombre"].ToString(),
                        Ambito = fila["ambito"].ToString()
                    };
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
    }
}
