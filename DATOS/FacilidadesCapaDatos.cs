using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DATOS
{
    public class FacilidadesCapaDatos
    {
        public SqlConnection conexion;
        public SqlTransaction transaccion;
        public string error;

        public FacilidadesCapaDatos()
        {
            this.conexion = Conexion.getConexion();
        }

        private List<ENTIDAD.Facilidades> listarFacilidades = new List<ENTIDAD.Facilidades>();
        public IEnumerable<ENTIDAD.Facilidades> ListadoFacilidades()
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                comando.Connection = conexion;
                conexion.Open();
                comando.CommandText = "exec PA_ListarFacilidades";

                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataSet ds = new DataSet();
                da.Fill(ds);

                ENTIDAD.Facilidades facilidades = null;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        facilidades = new ENTIDAD.Facilidades();
                        facilidades.nombre= (ds.Tables[0].Rows[i][1].ToString());
                        facilidades.reglas= (ds.Tables[0].Rows[i][2].ToString());
                        facilidades.detalles= (ds.Tables[0].Rows[i][4].ToString());
                        facilidades.urlImg = (ds.Tables[0].Rows[i][3].ToString());
                        listarFacilidades.Add(facilidades);
                    }
                }
                int result = comando.ExecuteNonQuery();

                return listarFacilidades;
            }
            catch (Exception) { }
            finally { conexion.Close(); }
            return listarFacilidades;
        }//Fin

        private List<ENTIDAD.Facilidades> listarFacilidades2 = new List<ENTIDAD.Facilidades>();
        public IEnumerable<ENTIDAD.Facilidades> ListadoFacilidades2()
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                comando.Connection = conexion;
                conexion.Open();
                comando.CommandText = "exec PA_ListarFacilidades";

                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataSet ds = new DataSet();
                da.Fill(ds);

                ENTIDAD.Facilidades facilidades = null;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        facilidades = new ENTIDAD.Facilidades();
                        facilidades.id_Facilidades = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                        facilidades.nombre = (ds.Tables[0].Rows[i][1].ToString());
                        facilidades.reglas = (ds.Tables[0].Rows[i][2].ToString());
                        facilidades.detalles = (ds.Tables[0].Rows[i][4].ToString());
                        facilidades.urlImg = (ds.Tables[0].Rows[i][3].ToString());
                        listarFacilidades2.Add(facilidades);
                    }
                }
                int result = comando.ExecuteNonQuery();

                return listarFacilidades2;
            }
            catch (Exception) { }
            finally { conexion.Close(); }
            return listarFacilidades2;
        }//Fin


        

                public int eliminarFacilidades(ENTIDAD.Facilidades facilidades)
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "exec PA_EliminarFacilidades @idFacilidades";
                comando.Parameters.AddWithValue("@idFacilidades", facilidades.id_Facilidades);
             
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
        public int modificarFacilidades(ENTIDAD.Facilidades facilidades)
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "exec PA_ModificarFacilidades @idFacilidades,@nombre,@reglas,@imagen,@detalles";
                comando.Parameters.AddWithValue("@idFacilidades", facilidades.id_Facilidades);
                comando.Parameters.AddWithValue("@nombre", facilidades.nombre);
                comando.Parameters.AddWithValue("@reglas", facilidades.reglas);
                comando.Parameters.AddWithValue("@imagen", facilidades.urlImg);
                comando.Parameters.AddWithValue("@detalles", facilidades.detalles);
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



        public int registrarFacilidad(ENTIDAD.Facilidades facilidad)
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "exec PA_RegistrarFacilidad @nombre,@reglas,@img,@detalles";
                comando.Parameters.AddWithValue("@nombre", facilidad.nombre);
                comando.Parameters.AddWithValue("@reglas", facilidad.reglas);
                comando.Parameters.AddWithValue("@img", facilidad.urlImg);
                comando.Parameters.AddWithValue("@detalles", facilidad.detalles);
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
            catch (Exception ex)
            {
                throw ex;
            }
            finally { conexion.Close(); }

        }//fin









    }
}
