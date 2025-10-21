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
    public class AD_EventoSismico
    {
        public static List<EventoSismico> BuscarTodosEventosSismicos()
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            List<EventoSismico> listaResultados = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM evento_sismico";

                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                listaResultados = new List<EventoSismico>();

                foreach (DataRow fila in tabla.Rows)
                {
                    EventoSismico nuevoEvento = new EventoSismico();

                    DateTime fechaHoraOcurrencia = Convert.ToDateTime(fila["fecha_hora_ocurrencia"]);
                    nuevoEvento.FechaOcurrencia = fechaHoraOcurrencia;
                    DateTime fechaHoraFin = Convert.ToDateTime(fila["fecha_hora_fin"]);
                    nuevoEvento.FechaHoraFin = fechaHoraFin;
                    nuevoEvento.LatitudEpicentro = Convert.ToSingle(fila["latitud_epicentro"]);
                    nuevoEvento.LatitudHipocentro = Convert.ToSingle(fila["latitud_hipocentro"]);
                    nuevoEvento.LongitudEpicentro = Convert.ToSingle(fila["longitud_epicentro"]);
                    nuevoEvento.LongitudHipocentro = Convert.ToSingle(fila["longitud_hipocentro"]);
                    nuevoEvento.ValorMagnitud = Convert.ToSingle(fila["valor_magnitud"]);

                    string nombreClasificacionSismo = Convert.ToString(fila["nombre_clasificacion_sismo"]);
                    nuevoEvento.Clasificacion = ObtenerClasificacionSismo(nombreClasificacionSismo);

                    string numeroMagnitudRichter = fila["numero_magnitud_richter"].ToString();
                    nuevoEvento.Magnitud = ObtenerMagnitudSismo(numeroMagnitudRichter);

                    string nombreOrigenGeneracion = Convert.ToString(fila["nombre_origen_generacion"]);
                    nuevoEvento.Origen = ObtenerOrigenSismo(nombreOrigenGeneracion);

                    string nombreAlcanceSismo = Convert.ToString(fila["nombre_alcance_sismo"]);
                    nuevoEvento.Alcance = ObtenerAlcanceSismo(nombreAlcanceSismo);

                    nuevoEvento.CambioEstado = ObtenerCambiosEstado(fechaHoraOcurrencia, fechaHoraFin);

                    nuevoEvento.SerieTemporal = ObtenerSeriesTemporales(fechaHoraOcurrencia, fechaHoraFin);

                    listaResultados.Add(nuevoEvento);
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

        private static ClasificacionSismo ObtenerClasificacionSismo(string nombreClasificacionSismo)
        {
            return AD_ClasificacionSismo.AgregarClasificacion(nombreClasificacionSismo);
        }

        private static MagnitudRichter ObtenerMagnitudSismo(string numeroMagnitudRichter)
        {
            return AD_MagnitudSismo.AgregarMagnitud(numeroMagnitudRichter);
        }

        private static OrigenDeGeneracion ObtenerOrigenSismo(string nombreOrigenGeneracion)
        {
            return AD_OrigenSismo.AgregarOrigen(nombreOrigenGeneracion);
        }

        private static AlcanceSismo ObtenerAlcanceSismo(string nombreAlcanceSismo)
        {
            return AD_AlcanceSismo.AgregarAlcance(nombreAlcanceSismo);
        }

        private static List<CambioEstado> ObtenerCambiosEstado (DateTime fechaHoraOcurrenciaEvento, DateTime fechaHoraFinEvento)
        {
            return AD_CambioEstado.AgregarCambiosEstado(fechaHoraOcurrenciaEvento, fechaHoraFinEvento);
        }

        private static List<SerieTemporal> ObtenerSeriesTemporales(DateTime fechaHoraOcurrenciaEvento, DateTime fechaHoraFinEvento)
        {
            return AD_SerieTemporal.AgregarSeries(fechaHoraOcurrenciaEvento, fechaHoraFinEvento);
        }
    }
}
