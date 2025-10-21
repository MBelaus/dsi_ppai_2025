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
    public class AD_MuestraSismica
    {
        public static List<MuestraSismica> AgregarMuestras(DateTime fechaHoraRegistroSerie, DateTime fechaHoraInicioRegistroSerie)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            List<MuestraSismica> listaResultados = new List<MuestraSismica>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT m.* FROM muestra_sismica m " +
                    "INNER JOIN serie_temporal s ON m.fecha_hora_registro_serie = s.fecha_hora_registro " +
                    "AND m.fecha_hora_inicio_muestra_serie = s.fecha_hora_inicio_muestras " +
                    "WHERE m.fecha_hora_registro_serie = @fechaHoraRegistroSerie " +
                    "AND m.fecha_hora_inicio_muestra_serie = @fechaHoraInicioRegistroSerie ";

                cmd.Parameters.AddWithValue("@fechaHoraRegistroSerie", fechaHoraRegistroSerie);
                cmd.Parameters.AddWithValue("@fechaHoraInicioRegistroSerie", fechaHoraInicioRegistroSerie);
                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                foreach (DataRow fila in tabla.Rows)
                {
                    MuestraSismica nuevaMuestra = new MuestraSismica();

                    DateTime fechaHoraMuestra = Convert.ToDateTime(fila["fecha_hora_muestra"]);
                    nuevaMuestra.FechaHoraMuestra = fechaHoraMuestra;

                    nuevaMuestra.DetalleMuestraSismica = ObtenerDetalleMuestra(fechaHoraMuestra, fechaHoraRegistroSerie, fechaHoraInicioRegistroSerie);

                    listaResultados.Add(nuevaMuestra);
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


        private static List<DetalleMuestraSismica> ObtenerDetalleMuestra(DateTime fechaHoraMuestra, DateTime fechaHoraRegistroSerie, DateTime fechaHoraInicioRegistroSerie)
        {
            return AD_DetalleMuestraSismica.AgregarDetalle(fechaHoraMuestra, fechaHoraRegistroSerie, fechaHoraInicioRegistroSerie);
        }
    }
}
