﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web;

namespace ENTIDAD
{
    public class Imagen 
    {
      
        [DisplayFormat(NullDisplayText = "Sin Respuesta")]
        [Display(Name = "Id Imagen")]
        public int idImagen { get; set; }

        [DisplayFormat(NullDisplayText = "Sin Respuesta")]
        [Display(Name = "Ruta")]
        public string imgPath { get; set; }
        public int tipo { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

        public Imagen(int idImagen, string imgPath)
        {
            this.idImagen = idImagen;
            this.imgPath = imgPath;
        }
        
        public Imagen(int tipo)
        {
            this.tipo = tipo;

        }
        public Imagen()
        {
         

        }
    }
}
