using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Text;

namespace NEGOCIO
{
    public class ReservacionCapaNegocios
    {
        DATOS.ReservacionCapaDatos reservacionCapaDatos= new DATOS.ReservacionCapaDatos();
        public int registrarReservacion(Reservacion reservacion)
        { return reservacionCapaDatos.registrarReservacion(reservacion); }

        public int verificarReservacion (Reservacion reservacion)
        { return reservacionCapaDatos.verificarReservacion(reservacion); }

        public int Precio(int id)
        { return reservacionCapaDatos.Precio(id); }

        public IEnumerable<ENTIDAD.Reservacion> sugerirReservacion()
        { return reservacionCapaDatos.sugerirReservacion(); }

        public IEnumerable<ENTIDAD.Reservacion> filtrandoReservacionById(ENTIDAD.Reservacion reservacion)
        { return reservacionCapaDatos.filtrandoReservacionById(reservacion); }

        public IEnumerable<ENTIDAD.Reservacion> listarReservaciones()
        { return reservacionCapaDatos.listarReservaciones(); }
        public IEnumerable<ENTIDAD.Reservacion> consultarReservaciones(ENTIDAD.Reservacion reserva)
        { return reservacionCapaDatos.consultarReservaciones(reserva); }

        public int eliminarReservacion(Reservacion reservacion)
        {
            return reservacionCapaDatos.eliminarReservacion(reservacion);
        }
        public int modificarReservacion(Reservacion reservacion)
        {
            return reservacionCapaDatos.modificarReservacion(reservacion);
        }
    }
}

