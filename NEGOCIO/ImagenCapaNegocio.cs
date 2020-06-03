using System;
using System.Collections.Generic;
using System.Text;

namespace NEGOCIO
{
   public  class ImagenCapaNegocio
    {

        DATOS.ImagenCapaDatos capaDatos = new DATOS.ImagenCapaDatos();


        public int modificarImagenes(ENTIDAD.Imagen imagen)
        {
            return capaDatos.modificarImagenes(imagen);
        }
        public int eliminarImagenes(ENTIDAD.Imagen imagen)
        {
            return capaDatos.eliminarImagenes(imagen);
        }
        public int registrarImagenes(ENTIDAD.Imagen imagen)
        {
            return capaDatos.registrarImagenes(imagen);
        }

        public IEnumerable<ENTIDAD.Imagen> listadoImagenes(ENTIDAD.Imagen imagen)
        {
            return capaDatos.listadoImagenes(imagen);
        }
        public IEnumerable<ENTIDAD.Imagen> listadoImagenesById(ENTIDAD.Imagen imagen)
        {
            return capaDatos.listadoImagenes(imagen);
        }

      
    }
}
