using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DATOS
{
   public class ImagenCapaDatos
    {

        public SqlConnection conexion;
        public SqlTransaction transaccion;
        public string error;

        public ImagenCapaDatos()
        {
            this.conexion = Conexion.getConexion();
        }


        public int modificarImagenes(ENTIDAD.Imagen imagen)
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandText = "exec PA_ModificarImagen @idImagen,@ruta";
                comando.Parameters.AddWithValue("@idImagen", imagen.idImagen);
                comando.Parameters.AddWithValue("@ruta", imagen.imgPath);
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

        private List<ENTIDAD.Imagen> listarImagenes = new List<ENTIDAD.Imagen>();
        public IEnumerable<ENTIDAD.Imagen> listadoImagenes(ENTIDAD.Imagen imagen)
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                comando.Connection = conexion;
                conexion.Open();
                comando.CommandText = "exec PA_ListarImagenes @tipo";
                comando.Parameters.AddWithValue("@tipo", imagen.tipo);
                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataSet ds = new DataSet();
                da.Fill(ds);

                ENTIDAD.Imagen galeria = null;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        galeria = new ENTIDAD.Imagen();
                        galeria.imgPath = (ds.Tables[0].Rows[i][0].ToString());
                        galeria.idImagen = int.Parse(ds.Tables[0].Rows[i][1].ToString());
                        listarImagenes.Add(galeria);
                    }
                }
                int result = comando.ExecuteNonQuery();

                return listarImagenes;
            }
            catch (Exception) { }
            finally { conexion.Close(); }
            return listarImagenes;
        }//Fin

    }
}
