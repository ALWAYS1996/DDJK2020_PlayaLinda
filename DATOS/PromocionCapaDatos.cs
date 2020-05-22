using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DATOS
{
    public class PromocionCapaDatos
    {
        public SqlConnection conexion;
        public SqlTransaction transaccion;
        public string error;

        public PromocionCapaDatos()
        {
            this.conexion = Conexion.getConexion();
        }
        private List<ENTIDAD.Promocion> listarPromociones = new List<ENTIDAD.Promocion>();

        public int createPromo(Promocion promocion)
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "exec PA_RegistrarPromocion @fechaInicio,@fechaFinal,@informacion,@rebaja";
                comando.Parameters.AddWithValue("@informacion", promocion.informacion);
                comando.Parameters.AddWithValue("@fechaInicio", promocion.fechaInicio);
                comando.Parameters.AddWithValue("@fechaFinal", promocion.fechaFinal);
                comando.Parameters.AddWithValue("@rebaja", promocion.precio);
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

          
        }

        public int updatePromo(Promocion promocion)
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "exec PA_ModificarPromocion @idPromocion,@fechaInicio,@fechaFinal,@informacion,@rebaja";
                comando.Parameters.AddWithValue("@idPromocion", promocion.codigoPromocion);
                comando.Parameters.AddWithValue("@fechaInicio", promocion.fechaInicio);
                comando.Parameters.AddWithValue("@fechaFinal", promocion.fechaFinal);
                comando.Parameters.AddWithValue("@informacion", promocion.informacion);
                comando.Parameters.AddWithValue("@rebaja", promocion.precio);

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
        }

        public Promocion obtenerPromoById(int codigoPromocion)
        {
            ENTIDAD.Promocion promocion = null;

            promocion = new ENTIDAD.Promocion();
            SqlCommand comando = new SqlCommand();
            try
            {
                comando.Connection = conexion;
                conexion.Open();
                comando.CommandText = "exec PA_ObtenerPromoById @codigoPromo";
                comando.Parameters.AddWithValue("@codigoPromo", codigoPromocion);
                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataSet ds = new DataSet();
                da.Fill(ds);

                
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {

                        promocion.codigoPromocion = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                        promocion.fechaInicio = DateTime.Parse(ds.Tables[0].Rows[i][1].ToString());
                        promocion.fechaFinal= DateTime.Parse(ds.Tables[0].Rows[i][2].ToString());
                        promocion.informacion= (ds.Tables[0].Rows[i][3].ToString());
                        promocion.precio= int.Parse(ds.Tables[0].Rows[i][4].ToString());
                        promocion.imgUrl = (ds.Tables[0].Rows[i][4].ToString());
                        
                    }
                }
                int result = comando.ExecuteNonQuery();

                return promocion;
            }
            catch (Exception) { }
            finally { conexion.Close(); }
            return promocion;
        }

        public int deletePromo(int codigoPromocion)
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "exec PA_EliminarPromocion @idPromocion";
                comando.Parameters.AddWithValue("@idPromocion", codigoPromocion);
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
        }

        public IEnumerable<ENTIDAD.Promocion> listadoPromociones()
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                comando.Connection = conexion;
                conexion.Open();
                comando.CommandText = "exec PA_ListarPromocion";

                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataSet ds = new DataSet();
                da.Fill(ds);

                ENTIDAD.Promocion promocion= null;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        promocion = new ENTIDAD.Promocion();
                        promocion.codigoPromocion= int.Parse(ds.Tables[0].Rows[i][0].ToString());
                        promocion.fechaInicio = DateTime.Parse(ds.Tables[0].Rows[i][1].ToString());
                        promocion.fechaFinal = DateTime.Parse(ds.Tables[0].Rows[i][2].ToString());
                        promocion.informacion = (ds.Tables[0].Rows[i][3].ToString());
                        promocion.precio = int.Parse(ds.Tables[0].Rows[i][4].ToString());
                        promocion.imgUrl =(ds.Tables[0].Rows[i][5].ToString());

                        listarPromociones.Add(promocion);
                    }
                }
                int result = comando.ExecuteNonQuery();

                return listarPromociones;
            }
            catch (Exception) { }
            finally { conexion.Close(); }
            return listarPromociones;
        }//Fin

    }
}
