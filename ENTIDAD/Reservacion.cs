using System;
using System.ComponentModel.DataAnnotations;

namespace ENTIDAD
{
    public class Reservacion 
    {
        [DisplayFormat(NullDisplayText = "Sin Respuesta")]
        [Display(Name = "Id Reserva")]
        [Key]
        public int codigoReservacion { get; set; }


        [DisplayFormat(NullDisplayText = "Sin Respuesta")]
        [Display(Name = "Id Habitacion")]

        public int codigoHabitacion { get; set; }

        //No quitar, la necesito para el calendario por eso es temporal
        public string idHabitacionTemp{ get; set; }

        public int idTipoHabitacion { get; set; }


        [DisplayFormat(NullDisplayText = "Sin Respuesta")]
        [Display(Name = "Id Cliente")]

        public int codigoCliente { get; set; }


        [Display(Name = "Fecha Llegada")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime fechaLlegada { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]


        //No quitar, la necesito para el calendario por eso es temporal
        public string fechaL { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]

        //No quitar, la necesito para el calendario por eso es temporal
        public string fechaS { get; set; }



        [DisplayFormat(NullDisplayText = "Sin Respuesta")]
        [Display(Name = "Fecha Salida")]

        public DateTime fechaSalida { get; set; }

        public Reservacion()
        {
        }

        public Reservacion(int codigoReservacion, int codigoHabitacion, int codigoCliente, DateTime fechaLlegada, DateTime fechaSalida)
        {
            this.codigoReservacion = codigoReservacion;
            this.codigoHabitacion = codigoHabitacion;
            this.codigoCliente = codigoCliente;
            this.fechaLlegada = fechaLlegada;
            this.fechaSalida = fechaSalida;
        }
        public Reservacion(string idTipoHabitacion, DateTime fechaLlegada, DateTime fechaSalida)
        {
            this.idHabitacionTemp = idTipoHabitacion;
            this.fechaLlegada = fechaLlegada;
            this.fechaSalida = fechaSalida;
        }

        public Reservacion(int idTipoHabitacion, DateTime fechaLlegada, DateTime fechaSalida)
        {
            this.idHabitacionTemp = idTipoHabitacion.ToString();
            this.fechaLlegada = fechaLlegada;
            this.fechaSalida = fechaSalida;
        }
        public Reservacion(int idReserva)
        {
            this.codigoReservacion = idReserva;

        }
       
    }
}
