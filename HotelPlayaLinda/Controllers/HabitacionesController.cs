using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ENTIDAD;

namespace HotelPlayaLinda.Controllers
{
    public class HabitacionesController : Controller
    {
        NEGOCIO.HabitacionCapaNegocio HabitacionesCapaNegocio = new NEGOCIO.HabitacionCapaNegocio();
        


        public ActionResult adm_habitaciones() {
            return View();        }

        public ActionResult disponibilidad_habitaciones()
        {
            return View(HabitacionesCapaNegocio.listadoTipoHabitaciones());

        }

        public ActionResult MostrarDisponibles(Reservacion reservacion) {
            return View(HabitacionesCapaNegocio.listarDisponibilidadHabitacion(reservacion));
        }
    }




}