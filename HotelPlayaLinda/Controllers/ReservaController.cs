using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelPlayaLinda.Controllers
{
    
    public class ReservaController : Controller
    {


        NEGOCIO.ReservacionCapaNegocios reservacionCapaNegocios = new NEGOCIO.ReservacionCapaNegocios();
        NEGOCIO.HabitacionCapaNegocio HabitacionesCapaNegocio = new NEGOCIO.HabitacionCapaNegocio();
        NEGOCIO.ClienteCapaNegocio clienteCapaNegocio = new NEGOCIO.ClienteCapaNegocio();
        
        public ActionResult ReservaLinea(Reservacion reservacion)
        {

            ViewBag.mensaje = "Habitación disponible para ser reservada" + reservacion.codigoHabitacion;
            return View();
        }


        public ActionResult Reservar()
        {
            return View(HabitacionesCapaNegocio.listadoTipoHabitaciones());
        }

        public ActionResult filtrandoReservacionById(int codigoReservacion)
        {
            return View("ActualizarReservacion", reservacionCapaNegocios.filtrandoReservacionById(new Reservacion(codigoReservacion)));
        }
        public ActionResult ActualizarReservacion(int codigoReservacion, int codigoHabitacion, int codigoCliente, DateTime fechaSalida, DateTime fechaLlegada)
        {
            if (reservacionCapaNegocios.modificarReservacion(new Reservacion(codigoReservacion, codigoHabitacion, codigoCliente, fechaLlegada, fechaSalida))>0) {
                ViewBag.mensaje = "Se ha actualizado correctamente";
            } else {
                ViewBag.mensaje = "Lo sentimos, no se ha podido actualizar";
            }

            return View("ActualizarReservacion", reservacionCapaNegocios.filtrandoReservacionById(new Reservacion(codigoReservacion)));
        }

        public ActionResult Estado(Reservacion reservacion)
        {

            if (reservacionCapaNegocios.verificarReservacion(reservacion)==-1)
            {
                ViewBag.mensaje = "Lo sentimos, el rango de fechas que seleccionaste se encuentran ocupadas. En este calendario podrás ver que fechas se encuentran disponibles";
                return View(reservacionCapaNegocios.sugerirReservacion());
            }
            else
            {
                ViewData["idHabitacion"] = reservacion.codigoHabitacion;
                ViewData["fechaInicio"] = reservacion.fechaLlegada;
                ViewData["fechaFin"] = reservacion.fechaSalida;
                ViewBag.mensaje = "Habitación disponible para ser reservada";
                return View("ReservaLinea");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ListaReservaciones()
        {
            return View(reservacionCapaNegocios.listarReservaciones());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ConsultarReservacion(int codigoReservacion)
        {

            return View(reservacionCapaNegocios.consultarReservaciones(new Reservacion(codigoReservacion)));
        }

        public ActionResult DatosUsuario(string codigoTipoHabitacion, string fechaLlegada, string fechaSalida)
        {
            int codigoHabitacion = reservacionCapaNegocios.verificarReservacion(new Reservacion(codigoTipoHabitacion, fechaLlegada, fechaSalida));
            if (codigoHabitacion == -1)
            {
                ViewBag.mensaje = "Lo sentimos, el rango de fechas que seleccionaste se encuentran ocupadas. En este calendario podrás ver que fechas se encuentran disponibles:";
                ViewData["codigoTipoHabitacion"] = codigoHabitacion;
                return View("Estado", reservacionCapaNegocios.sugerirReservacion());
            }
            else
            {
                var resultado = (reservacionCapaNegocios.verificarReservacion(new Reservacion(codigoTipoHabitacion, fechaLlegada, fechaSalida)));
                Reservacion reservacion = new Reservacion();
                ViewData["codigoTipoHabitacion"] = codigoTipoHabitacion;
                ViewData["codigoHabitacion"] = codigoHabitacion;
                ViewData["fechaInicio"] =fechaLlegada;
                ViewData["fechaFin"] = fechaSalida;
                ViewData["NumeroHabitacion"] = resultado;
                ViewBag.mensaje = "Habitación disponible para ser reservada";
                return View();
            }
        }

        public ActionResult GenerarReservacion(int codigoHabitacion, string fechaInicio, string fechaFin, string pasaporte, string nombre, string apellido, string email, string tarjeta)
        {
            Cliente cliente = new Cliente();
            cliente.apellido1 = apellido;
            cliente.nombre = nombre;
            cliente.pasaporte = pasaporte;
            cliente.correo = email;
            clienteCapaNegocio.registrarCliente(cliente);

            Reservacion reservacion = new Reservacion();
            reservacion.codigoHabitacion = codigoHabitacion;
            reservacion.codigoCliente = Int32.Parse(pasaporte);
            reservacion.fechaLlegada = Convert.ToDateTime(fechaInicio);
            reservacion.fechaSalida = Convert.ToDateTime(fechaFin);

            if (reservacionCapaNegocios.registrarReservacion(reservacion) == 1)
            {
                return View("Reservado");
            }
            return View("Cancelado");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult EliminarReservacion(int reservaid)
        {

            if (reservacionCapaNegocios.eliminarReservacion(new Reservacion(reservaid)) >= 1)
            {
                ViewBag.mensaje = "Eliminado";
            }
            else
            {
                ViewBag.mensaje = "No Eliminado";
            }
            return View("ListaReservaciones", reservacionCapaNegocios.listarReservaciones());

        }
    }
}