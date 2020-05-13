using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DATOS
{
    public class HabitacionesCapaDatos
    {

        public SqlConnection conexion;
        public SqlTransaction transaccion;
        public string error;

        public HabitacionesCapaDatos()
        {
            this.conexion = Conexion.getConexion();
        }

        private List<ENTIDAD.TipoHabitacion> listarTipoHabitacion = new List<ENTIDAD.TipoHabitacion>();
        public IEnumerable<ENTIDAD.TipoHabitacion> listadoTipoHabitaciones()
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                comando.Connection = conexion;
                conexion.Open();
                comando.CommandText = "exec PA_ListarTipoHabitacion";

                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataSet ds = new DataSet();
                da.Fill(ds);

                ENTIDAD.TipoHabitacion tipoHabitacion = null;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        tipoHabitacion = new ENTIDAD.TipoHabitacion();
                        tipoHabitacion.codigoTipoHabitacion = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                        tipoHabitacion.nombre = (ds.Tables[0].Rows[i][1].ToString());
                        tipoHabitacion.precio = int.Parse(ds.Tables[0].Rows[i][2].ToString());
                        tipoHabitacion.urlImg =(ds.Tables[0].Rows[i][3].ToString());
                        tipoHabitacion.descripcion = (ds.Tables[0].Rows[i][4].ToString());

                        listarTipoHabitacion.Add(tipoHabitacion);
                    }
                }
                int result = comando.ExecuteNonQuery();

                return listarTipoHabitacion;
            }
            catch (Exception) { }
            finally { conexion.Close(); }
            return listarTipoHabitacion;
        }//Fin


        private List<ENTIDAD.Habitacion> listarDisponibilidadHabitacion = new List<ENTIDAD.Habitacion>();
        public IEnumerable<ENTIDAD.Habitacion> listadoDisponibilidadHabitaciones(Reservacion reservacion)
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                comando.Connection = conexion;
                conexion.Open();
                comando.CommandText = "exec PA_DisponibilidadHabitaciones @idTipoHabitacion,@fechaLlegada,@fechaSalida";

                comando.Parameters.AddWithValue("@idTipoHabitacion", reservacion.idTipoHabitacion);
                comando.Parameters.AddWithValue("@fechaLlegada", reservacion.fechaLlegada);
                comando.Parameters.AddWithValue("@fechaSalida", reservacion.fechaSalida);

                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataSet ds = new DataSet();
                da.Fill(ds);

                ENTIDAD.Habitacion habitacionDisp = null;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        habitacionDisp = new ENTIDAD.Habitacion();
                        habitacionDisp.codigoHabitacion = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                        habitacionDisp.idTipoHabitacion = int.Parse(ds.Tables[0].Rows[i][1].ToString());
                        habitacionDisp.capacidad = int.Parse(ds.Tables[0].Rows[i][2].ToString());

                        listarDisponibilidadHabitacion.Add(habitacionDisp);
                    }
                }
                int result = comando.ExecuteNonQuery();

                return listarDisponibilidadHabitacion;
            }
            catch (Exception) { }
            finally { conexion.Close(); }
            return listarDisponibilidadHabitacion;
        }//Fin

        private List<ENTIDAD.Habitacion> listarHabitacionTipoHabitacion = new List<ENTIDAD.Habitacion>();
        public IEnumerable<ENTIDAD.Habitacion> listandoHabitacionTipoHabitacion(Habitacion habitacion)
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                comando.Connection = conexion;
                conexion.Open();
                comando.CommandText = "exec PA_ListarHabitacionTipoHabitacion @idTipoHabitacion";

                comando.Parameters.AddWithValue("@idTipoHabitacion", habitacion.idTipoHabitacion);

                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataSet ds = new DataSet();
                da.Fill(ds);

                ENTIDAD.Habitacion habitacionDisp = null;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        habitacionDisp = new ENTIDAD.Habitacion();
                        habitacionDisp.codigoHabitacion = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                        habitacionDisp.vacante = int.Parse(ds.Tables[0].Rows[i][1].ToString());
                        //    habitacionDisp.capacidad = int.Parse(ds.Tables[0].Rows[i][2].ToString());
                        //    habitacionDisp.nombreTipoHabitacion = ds.Tables[0].Rows[i][3].ToString();
                        //    habitacionDisp.idTipoHabitacion = int.Parse(ds.Tables[0].Rows[i][4].ToString());

                        listarHabitacionTipoHabitacion.Add(habitacionDisp);
                    }
                }
                int result = comando.ExecuteNonQuery();

                return listarHabitacionTipoHabitacion;
            }
            catch (Exception) { }
            finally { conexion.Close(); }
            return listarHabitacionTipoHabitacion;
        }//Fin


        public int modificarEstadoHabitacion(ENTIDAD.Habitacion habitacion)
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "exec PA_ModificarHabitacion @idHabitacion,@estado";
                comando.Parameters.AddWithValue("@idHabitacion", habitacion.codigoHabitacion);
                comando.Parameters.AddWithValue("@estado", habitacion.vacante);
                int result = comando.ExecuteNonQuery();
                if (result == -1)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception) { }
            finally { conexion.Close(); }
            return 0;
        }//fin
    }
}
