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
    public class AD_DetalleMuestraSismica
    {
        public static List<DetalleMuestraSismica> AgregarDetalle(DateTime fechaHoraMuestra, DateTime fechaHoraRegistroSerie, DateTime fechaHoraInicioMuestraSerie)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            List<DetalleMuestraSismica> listaResultados = new List<DetalleMuestraSismica>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT dm.* FROM detalle_muestra_sismica dm " +
                    "INNER JOIN tipo_de_dato td ON dm.denominacion_tipo_dato = td.denominacion " +
                    "AND dm.nombre_unidad_medida_tipo_dato = td.nombre_unidad_medida " +
                    "INNER JOIN muestra_sismica ms ON dm.fecha_hora_muestra_sismica = ms.fecha_hora_muestra " +
                    "AND dm.fecha_hora_registro_serie_muestra_sismica = ms.fecha_hora_registro_serie " +
                    "AND dm.fecha_hora_inicio_muestra_serie_detalle = ms.fecha_hora_inicio_muestra_serie " +
                    "WHERE dm.fecha_hora_muestra_sismica = @fechaHoraMuestra " +
                    "AND dm.fecha_hora_registro_serie_muestra_sismica = @fechaHoraRegistroSerie " +
                    "AND dm.fecha_hora_inicio_muestra_serie_detalle = @fechaHoraInicioMuestraSerie ";

                cmd.Parameters.AddWithValue("@fechaHoraMuestra", fechaHoraMuestra);
                cmd.Parameters.AddWithValue("@fechaHoraRegistroSerie", fechaHoraRegistroSerie);
                cmd.Parameters.AddWithValue("@fechaHoraInicioMuestraSerie", fechaHoraInicioMuestraSerie);
                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                foreach (DataRow fila in tabla.Rows)
                {
                    DetalleMuestraSismica nuevoDetalle = new DetalleMuestraSismica();
                    nuevoDetalle.Valor = Convert.ToSingle(fila["valor"]);

                    string denominacionTipoDato = Convert.ToString(fila["denominacion_tipo_dato"]);
                    string unidadDeMedidaTipoDato = Convert.ToString(fila["nombre_unidad_medida_tipo_dato"]);
                    nuevoDetalle.TipoDato = ObtenerTipoDato(denominacionTipoDato, unidadDeMedidaTipoDato);

                    listaResultados.Add(nuevoDetalle);
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

        private static TipoDeDato ObtenerTipoDato(string denominacionTipoDato, string unidadDeMedidaTipoDato)
        {
            return AD_TipoDato.AgregarTipoDato(denominacionTipoDato, unidadDeMedidaTipoDato);
        }
    }
}
