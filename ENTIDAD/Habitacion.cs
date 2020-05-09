using System.ComponentModel.DataAnnotations;

namespace ENTIDAD
{

    public class Habitacion
    {

        [DisplayFormat(NullDisplayText = "Sin Respuesta")]
        [Display(Name = "Código")]
        [Key]
        public int codigoHabitacion { get; set; }

        [DisplayFormat(NullDisplayText = "Sin Respuesta")]
        [Display(Name = "tipo Habitacion")]

        public string tipoHabitacion { get; set; }

        [DisplayFormat(NullDisplayText = "Sin Respuesta")]
        [Display(Name = "Estado Habitacion")]

        public int vacante { get; set; }


        [DisplayFormat(NullDisplayText = "Sin Respuesta")]
        [Display(Name = "Capacidad")]

        public int precio { get; set; }

        public Habitacion()
        {
        }

        public Habitacion(int codigoHabitacion, string tipoHabitacion, int vacante, int precio)
        {
            this.codigoHabitacion = codigoHabitacion;
            this.tipoHabitacion = tipoHabitacion;
            this.vacante = vacante;
            this.precio = precio;
        }
    }
}
