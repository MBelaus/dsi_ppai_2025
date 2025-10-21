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
    public class AD_EstacionSismologica
    {
        public static EstacionSismologica AgregarEstacion(int codigoEstacion)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            EstacionSismologica listaResultados = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM estacion_sismologica WHERE codigo_estacion = @codigoEstacion";

                cmd.Parameters.AddWithValue("@codigoEstacion", codigoEstacion);
                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    DataRow fila = tabla.Rows[0];
                    listaResultados = new EstacionSismologica
                    {
                        CodigoEstacion = Convert.ToInt32(fila["codigo_estacion"]),
                        DocumentoCertificacionAdq = fila["documento_certificado"].ToString(),
                        FechaSolicitudCertificacion = Convert.ToDateTime(fila["fecha_solicitud_certificado"]),
                        Longitud = Convert.ToSingle(fila["longitud"]),
                        Latitud = Convert.ToSingle(fila["latitud"]),
                        Nombre = fila["nombre"].ToString(),
                        NumeroCertificadoAdquisicion = Convert.ToInt32(fila["numero_certificado_adquisicion"])
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
