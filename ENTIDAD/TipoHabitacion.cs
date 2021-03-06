﻿using System.ComponentModel.DataAnnotations;

namespace ENTIDAD
{

    public class TipoHabitacion 
    {
        [DisplayFormat(NullDisplayText = "Sin Respuesta")]
        [Display(Name = "Codigo")]
        [Key]
        public int codigoTipoHabitacion { get; set; }


        [DisplayFormat(NullDisplayText = "Sin Respuesta")]
        [Display(Name = "Nombre")]

        public string nombre { get; set; }


        [DisplayFormat(NullDisplayText = "Sin Respuesta")]
        [Display(Name = "Precio")]
        public int precio { get; set; }

        [DisplayFormat(NullDisplayText = "Sin Respuesta")]
        [Display(Name = "urlImg")]
        public string urlImg { get; set; }

        [DisplayFormat(NullDisplayText = "Sin Respuesta")]
        [Display(Name = "descripcion")]
        public string descripcion { get; set; }

        public TipoHabitacion()
        {
        }
        public TipoHabitacion(int codigoTipoHabitacion)
        {

            this.codigoTipoHabitacion = codigoTipoHabitacion;
        }
        public TipoHabitacion(int codigoTipoHabitacion,string img)
        {

            this.codigoTipoHabitacion = codigoTipoHabitacion;
            this.urlImg = img;
        }
        public TipoHabitacion(int codigoTipoHabitacion, string nombre, int precio)
        {
            this.codigoTipoHabitacion = codigoTipoHabitacion;
            this.nombre = nombre;
            this.precio = precio;
        }

        public TipoHabitacion(int codigoTipoHabitacion, string nombre, int precio, string descripcion) : this(codigoTipoHabitacion, nombre, precio)
        {
           
            this.descripcion = descripcion;
        }
    }
}
