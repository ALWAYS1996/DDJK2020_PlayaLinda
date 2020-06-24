using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEGOCIO
{
   public class PublicidadCapaNegocio
    {
        DATOS.PublicidadCapaDatos publicidad = new DATOS.PublicidadCapaDatos();
        public IEnumerable<ENTIDAD.Publicidad> listadoPublicidad()
        { return publicidad.listadoPublicidad(); }

        public int actualizarPublicidad(int id, string imagen, string url, string nombre)
        {
            return publicidad.actualizarPublicidad(id, imagen, url, nombre);
        }
    }
}
