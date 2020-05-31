using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDAD
{
   public class Publicidad
    {

        public int idPublicidad { get; set; }
        public string rutaImg { get; set; }
        public string link { get; set; }


        public string descripcion { get; set; }

        public Publicidad()
        {
        }

        public Publicidad(int idPublicidad, string imgPath, string link, string descripcion)
        {
            this.idPublicidad = idPublicidad;
            this.rutaImg = imgPath;
            this.link = link;
            this.descripcion = descripcion;
        }
    }
}
