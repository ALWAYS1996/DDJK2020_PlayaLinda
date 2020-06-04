using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DATOS
{
   public class ItinerarioCapaDatos
    {
        public SqlConnection conexion;
        public SqlTransaction transaccion;
        public string error;

        public ItinerarioCapaDatos()
        {
            this.conexion = Conexion.getConexion();
        }




        private List<ENTIDAD.Itinerario> listarItinerario = new List<ENTIDAD.Itinerario>();
        public IEnumerable<ENTIDAD.Itinerario> listadoItinerario()
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                comando.Connection = conexion;
                conexion.Open();
                comando.CommandText = "exec PA_ListarItinerario";
                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataSet ds = new DataSet();
                da.Fill(ds);

                ENTIDAD.Itinerario itinerario = null;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        itinerario = new ENTIDAD.Itinerario();
                        itinerario.dia = (ds.Tables[0].Rows[i][0].ToString());
                        itinerario.desayuno = (ds.Tables[0].Rows[i][1].ToString());
                        itinerario.imgUrlDesayuno = (ds.Tables[0].Rows[i][2].ToString());
                        itinerario.almuerzo = (ds.Tables[0].Rows[i][3].ToString());
                        itinerario.imgUrlAlmuerzo = (ds.Tables[0].Rows[i][4].ToString());
                        itinerario.cena = (ds.Tables[0].Rows[i][5].ToString());
                        itinerario.imgUrlCena = (ds.Tables[0].Rows[i][6].ToString());
                        listarItinerario.Add(itinerario);
                    }
                }
                int result = comando.ExecuteNonQuery();

                return listarItinerario;
            }
            catch (Exception) { }
            finally { conexion.Close(); }
            return listarItinerario;
        }//Fin

        public IEnumerable<ENTIDAD.Itinerario> listadoItinerario2()
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                comando.Connection = conexion;
                conexion.Open();
                comando.CommandText = "exec PA_ListarItinerario2";
                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataSet ds = new DataSet();
                da.Fill(ds);

                ENTIDAD.Itinerario itinerario = null;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        itinerario = new ENTIDAD.Itinerario();
                        itinerario.idItinerario = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                        itinerario.dia = (ds.Tables[0].Rows[i][1].ToString());
                        itinerario.desayuno = (ds.Tables[0].Rows[i][2].ToString());
                        itinerario.imgUrlDesayuno = (ds.Tables[0].Rows[i][3].ToString());
                        itinerario.almuerzo = (ds.Tables[0].Rows[i][4].ToString());
                        itinerario.imgUrlAlmuerzo = (ds.Tables[0].Rows[i][5].ToString());
                        itinerario.cena = (ds.Tables[0].Rows[i][6].ToString());
                        itinerario.imgUrlCena = (ds.Tables[0].Rows[i][7].ToString());
                        listarItinerario.Add(itinerario);
                    }
                }
                int result = comando.ExecuteNonQuery();

                return listarItinerario;
            }
            catch (Exception) { }
            finally { conexion.Close(); }
            return listarItinerario;
        }//Fin



        public int modificarItinerario(ENTIDAD.Itinerario itinerario)
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "exec PA_ModificarItinerario @idItinerario,@dia,@desayuno,@imgDesayuno,@almuerzo,@imgAlmuerzo,@cena,@imgCena";
                comando.Parameters.AddWithValue("@idItinerario", itinerario.idItinerario);
                comando.Parameters.AddWithValue("@dia", itinerario.dia);
                comando.Parameters.AddWithValue("@desayuno", itinerario.desayuno);
                comando.Parameters.AddWithValue("@imgDesayuno", itinerario.imgUrlDesayuno);
                comando.Parameters.AddWithValue("@almuerzo", itinerario.almuerzo);
                comando.Parameters.AddWithValue("@imgAlmuerzo", itinerario.imgUrlAlmuerzo);
                comando.Parameters.AddWithValue("@cena", itinerario.cena);
                comando.Parameters.AddWithValue("@imgCena", itinerario.imgUrlCena);
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
