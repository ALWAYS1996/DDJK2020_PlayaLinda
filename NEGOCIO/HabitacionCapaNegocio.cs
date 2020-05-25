using System;
using System.Collections.Generic;
using DATOS;
using ENTIDAD;
using System.Text;

namespace NEGOCIO
{
    public class HabitacionCapaNegocio
    {
        DATOS.HabitacionesCapaDatos habitacionCapaDatos = new DATOS.HabitacionesCapaDatos();


        public IEnumerable<ENTIDAD.Habitacion> listarEstadoHoyHabitacion()
        {
            return habitacionCapaDatos.listandoEstadoHoyHabitacion();
        }
        public IEnumerable<ENTIDAD.TipoHabitacion> filtrandoTipoHabitaciones(TipoHabitacion habitacion)
        {
            return habitacionCapaDatos.filtrandoTipoHabitaciones(habitacion);
        }
        public IEnumerable<ENTIDAD.TipoHabitacion> listadoTipoHabitaciones()
        {
            return habitacionCapaDatos.listadoTipoHabitaciones();
        }
        public IEnumerable<ENTIDAD.Habitacion> listarDisponibilidadHabitacion(Reservacion reservacion)
        {
            return habitacionCapaDatos.listadoDisponibilidadHabitaciones(reservacion);
        }

        public IEnumerable<ENTIDAD.Habitacion> listandoHabitacionTipoHabitacion(Habitacion habitacion)
        {
            return habitacionCapaDatos.listandoHabitacionTipoHabitacion(habitacion);
        }
        public int modificarEstado(Habitacion habitacion)
        {
            return habitacionCapaDatos.modificarEstadoHabitacion(habitacion);
        }
        public int modificarTipoHabitacion(TipoHabitacion habitacion)
        {
            return habitacionCapaDatos.modificarTipoHabitacion(habitacion);
        }
        public int modificarImagenTipoHabitacion(TipoHabitacion habitacion)
        {
            return habitacionCapaDatos.modificarImagenTipoHabitacion(habitacion);
        }
        
    }

        
}
