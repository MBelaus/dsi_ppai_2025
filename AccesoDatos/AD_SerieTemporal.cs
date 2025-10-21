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
    public class AD_SerieTemporal
    {
        public static List<SerieTemporal> AgregarSeries(DateTime fechaHoraOcurrenciaEvento, DateTime fechaHoraFinEvento)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            List<SerieTemporal> listaResultados = new List<SerieTemporal>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT st.* FROM serie_temporal st " +
                    "INNER JOIN sismografo s ON st.id_sismografo = s.id_sismografo " +
                    "INNER JOIN evento_sismico e ON st.fecha_hora_ocurrencia_evento = e.fecha_hora_ocurrencia " +
                    "AND st.fecha_hora_fin_evento = e.fecha_hora_fin " +
                    "WHERE st.fecha_hora_ocurrencia_evento = @fechaHoraOcurrenciaEvento " +
                    "AND st.fecha_hora_fin_evento = @fechaHoraFinEvento";

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
                    SerieTemporal nuevaSerie = new SerieTemporal();

                    nuevaSerie.CondicionAlarma = fila["condicion_alarma"].ToString();
                    DateTime fechaHoraRegistro = Convert.ToDateTime(fila["fecha_hora_registro"]);
                    nuevaSerie.FechaHoraRegistro = fechaHoraRegistro;
                    DateTime fechaHoraInicioMuestras = Convert.ToDateTime(fila["fecha_hora_inicio_muestras"]);
                    nuevaSerie.FechaHoraInicioRegistroMuestras = fechaHoraInicioMuestras;
                    nuevaSerie.FrecuenciaMuestreo = fila["frecuencia_muestras"].ToString();

                    int idSismografo = Convert.ToInt32(fila["id_sismografo"]);
                    ObtenerSismografo(idSismografo);

                    nuevaSerie.Muestras = ObtenerMuestrasSismicas(fechaHoraRegistro, fechaHoraInicioMuestras);

                    listaResultados.Add(nuevaSerie);
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

        public static List<SerieTemporal> ObtenerSeriesTemporales(int idSismografo)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            List<SerieTemporal> listaResultados = new List<SerieTemporal>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT st.* FROM serie_temporal st " +
                    "INNER JOIN sismografo s ON st.id_sismografo = s.id_sismografo " +
                    "WHERE st.id_sismografo = @idSismografo";

                cmd.Parameters.AddWithValue("@idSismografo", idSismografo);
                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                foreach (DataRow fila in tabla.Rows)
                {
                    SerieTemporal nuevaSerie = new SerieTemporal();

                    nuevaSerie.CondicionAlarma = fila["condicion_alarma"].ToString();
                    nuevaSerie.FechaHoraRegistro = Convert.ToDateTime(fila["fecha_hora_registro"]);
                    nuevaSerie.FechaHoraInicioRegistroMuestras = Convert.ToDateTime(fila["fecha_hora_inicio_muestras"]);
                    nuevaSerie.FrecuenciaMuestreo = fila["frecuencia_muestras"].ToString();

                    listaResultados.Add(nuevaSerie);
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

        private static List<MuestraSismica> ObtenerMuestrasSismicas(DateTime fechaHoraRegistro, DateTime fechaHoraInicioMuestras)
        {
            return AD_MuestraSismica.AgregarMuestras (fechaHoraRegistro, fechaHoraInicioMuestras);
        }

        private static Sismografo ObtenerSismografo(int idSismografo)
        {
            return AD_Sismografo.AgregarSismografo (idSismografo);
        }
    }
}
