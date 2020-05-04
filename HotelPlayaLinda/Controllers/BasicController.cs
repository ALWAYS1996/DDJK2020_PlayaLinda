using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelPlayaLinda.Controllers
{
    public class BasicController : Controller
    {
        // GET: Basic

        NEGOCIO.FacilidadesCapaNegocio FacilidadesCapaNegocio = new NEGOCIO.FacilidadesCapaNegocio();
        NEGOCIO.HabitacionCapaNegocio HabitacionesCapaNegocio = new NEGOCIO.HabitacionCapaNegocio();


        public ActionResult Facilidades()
        {
            ENTIDAD.Facilidades tipo = new ENTIDAD.Facilidades();
            //ViewData["listaTipoHabitaciones"] = habitacionCapaNegocio.listadoTipoHabitaciones();
            return View(FacilidadesCapaNegocio.listadoFacilidades());
        }



        public ActionResult Tarifas()
        {
            return View(HabitacionesCapaNegocio.listadoTipoHabitaciones());
        }
    }
}