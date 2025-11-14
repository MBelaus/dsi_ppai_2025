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
    public class AD_CambioEstado
    {
        public static List<CambioEstado> AgregarCambiosEstado(DateTime fechaHoraOcurrenciaEvento, DateTime fechaHoraFinEvento)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            List<CambioEstado> listaResultados = new List<CambioEstado>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT ce.* FROM cambio_estado ce " +
                    "INNER JOIN evento_sismico es ON ce.fecha_hora_ocurrencia_evento = es.fecha_hora_ocurrencia " +
                    "AND ce.fecha_hora_fin_evento = es.fecha_hora_fin " +
                    "INNER JOIN estado e ON ce.ambito_estado = e.ambito and ce.nombre_estado = e.nombre " +
                    "WHERE ce.fecha_hora_ocurrencia_evento = @fechaHoraOcurrenciaEvento AND ce.fecha_hora_fin_evento = @fechaHoraFinEvento";

                cmd.Parameters.AddWithValue("@fechaHoraOcurrenciaEvento", fechaHoraOcurrenciaEvento);
                cmd.Parameters.AddWithValue("@fechaHoraFinEvento", fechaHoraFinEvento);
                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                foreach (DataRow fila in tabla.Rows)
                {
                    CambioEstado nuevoEstado = new CambioEstado();

                    nuevoEstado.FechaHoraFin = fila["fecha_hora_fin"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(fila["fecha_hora_fin"]);
                    nuevoEstado.FechaHoraInicio = Convert.ToDateTime(fila["fecha_hora_inicio"]);

                    string ambitoEstado = fila["ambito_estado"].ToString();
                    string nombreEstado = fila["nombre_estado"].ToString();
                    nuevoEstado.EstadoActual = ObtenerEstadoSismo(ambitoEstado, nombreEstado);

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

        public static void ActualizarCambioEstado(DateTime? fechaHoraFin, DateTime fechaHoraOcurrenciaEvento, DateTime fechaHoraFinEvento, DateTime? fechaHoraInicio)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "UPDATE cambio_estado SET fecha_hora_fin = @fechaHoraFin " +
                    "WHERE fecha_hora_ocurrencia_evento = @fechaHoraOcurrenciaEvento " +
                    "AND fecha_hora_fin_evento = @fechaHoraFinEvento " +
                    "AND fecha_hora_inicio = @fechaHoraInicio";

                cmd.Parameters.AddWithValue("@fechaHoraFin", fechaHoraFin);
                cmd.Parameters.AddWithValue("@fechaHoraOcurrenciaEvento", fechaHoraOcurrenciaEvento);
                cmd.Parameters.AddWithValue("@fechaHoraFinEvento", fechaHoraFinEvento);
                cmd.Parameters.AddWithValue("@fechaHoraInicio", fechaHoraInicio);

                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el cambio de estado: " + ex.Message);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
        }

        public static void InsertarNuevoCambioEstado(DateTime fechaOcurrenciaEvento, DateTime fechaHoraFinEvento, DateTime fechaHoraInicio, string ambitoEstado, string nombreEstado)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "INSERT INTO cambio_estado " +
                    "VALUES (@fechaHoraInicio, NULL, @ambitoEstado, @nombreEstado, @fechaOcurrenciaEvento, @fechaHoraFinEvento)";

                cmd.Parameters.AddWithValue("@fechaHoraInicio", fechaHoraInicio);
                cmd.Parameters.AddWithValue("@ambitoEstado", ambitoEstado);
                cmd.Parameters.AddWithValue("@nombreEstado", nombreEstado);
                cmd.Parameters.AddWithValue("@fechaOcurrenciaEvento", fechaOcurrenciaEvento);
                cmd.Parameters.AddWithValue("@fechaHoraFinEvento", fechaHoraFinEvento);

                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar el cambio de estado: " + ex.Message);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
        }

        private static Estado ObtenerEstadoSismo(string ambitoEstado, string nombreEstado)
        {
            return AD_Estado.AgregarEstado(ambitoEstado, nombreEstado);
        }
    }
}

