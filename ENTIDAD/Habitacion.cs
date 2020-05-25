using System.ComponentModel.DataAnnotations;

namespace ENTIDAD
{

    public class Habitacion : TipoHabitacion
    {

        [DisplayFormat(NullDisplayText = "Sin Respuesta")]
        [Display(Name = "Código")]
        [Key]
        public int codigoHabitacion { get; set; }

        [DisplayFormat(NullDisplayText = "Sin Respuesta")]
        [Display(Name = "tipo Habitacion")]

        public int idTipoHabitacion { get; set; }

        [DisplayFormat(NullDisplayText = "Sin Respuesta")]
        [Display(Name = "Estado Habitacion")]

        public int vacante { get; set; }


        [DisplayFormat(NullDisplayText = "Sin Respuesta")]
        [Display(Name = "Capacidad")]

        public int capacidad { get; set; }

        public Habitacion()
        {
        }
        public Habitacion(int idTipoHabitacion)
        {
            this.idTipoHabitacion = idTipoHabitacion;
        }
        public Habitacion(int codigoHabitacion, int idTipoHabitacion, int vacante, int capacidad)
        {
            this.codigoHabitacion = codigoHabitacion;
            this.idTipoHabitacion = idTipoHabitacion;
            this.vacante = vacante;
            this.capacidad = capacidad;
        }
        public Habitacion(int codigoHabitacion, int vacante)
        {
            this.codigoHabitacion = codigoHabitacion;
            this.vacante = vacante;
        }
    }
}
