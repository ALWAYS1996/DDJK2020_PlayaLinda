using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
   public class PublicidadCapaDatos
    {

        public  SqlConnection conexion;
        public SqlTransaction transaccion;
        public string error;

        public PublicidadCapaDatos()
        {
            this.conexion = Conexion.getConexion();
        }



    private List<ENTIDAD.Publicidad> listarPublicidad = new List<ENTIDAD.Publicidad>();
        public IEnumerable<ENTIDAD.Publicidad> listadoPublicidad()
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                comando.Connection = conexion;
                conexion.Open();
                comando.CommandText = "exec PA_ListarPublicidad";

                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataSet ds = new DataSet();
                da.Fill(ds);

                ENTIDAD.Publicidad publicidad = null;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        publicidad = new ENTIDAD.Publicidad();
                        publicidad.idPublicidad = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                        publicidad.rutaImg = (ds.Tables[0].Rows[i][1].ToString());
                        publicidad.link = (ds.Tables[0].Rows[i][2].ToString());
                        publicidad.descripcion = (ds.Tables[0].Rows[i][3].ToString());
                     
                        listarPublicidad.Add(publicidad);
                    }
                }
                int result = comando.ExecuteNonQuery();

                return listarPublicidad;
            }
            catch (Exception) { }
            finally { conexion.Close(); }
            return listarPublicidad;
        }//Fin

    }
}
