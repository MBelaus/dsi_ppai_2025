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
    public class AD_ClasificacionSismo
    {
        public static ClasificacionSismo AgregarClasificacion(string nombreClasificacion)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            ClasificacionSismo listaResultados = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM clasificacion_sismo WHERE nombre = @nombreClasificacion";

                cmd.Parameters.AddWithValue("@nombreClasificacion", nombreClasificacion);
                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    DataRow fila = tabla.Rows[0];
                    listaResultados = new ClasificacionSismo
                    {
                        KmProfundidadDesde = Convert.ToSingle(fila["km_profundidad_desde"]),
                        KmProfundidadHasta = Convert.ToSingle(fila["km_profundidad_hasta"]),
                        Nombre = fila["nombre"].ToString()
                    };
                }


            }
            catch(Exception ex)
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
