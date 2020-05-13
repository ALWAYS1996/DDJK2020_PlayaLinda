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


        [Authorize(Roles = "Admin")]
        public ActionResult adm_habitaciones()
        {
            ViewData["tipoHabitacion"] = HabitacionesCapaNegocio.listadoTipoHabitaciones();
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult filtrarHabitaciones(int idTipoHabitacion)
        {
            ViewData["IdHabit"] = idTipoHabitacion;
            ViewData["tipoHabitacion"] = HabitacionesCapaNegocio.listadoTipoHabitaciones();
            return View("modificarHabitaciones", HabitacionesCapaNegocio.listandoHabitacionTipoHabitacion(new Habitacion(idTipoHabitacion)));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult modificarHabitaciones(int idTipoHa, int codigoHabitacion, int[] estado)
        {
            var count = estado.Length;
            @ViewData["IdHabit"] = idTipoHa;

            for (var i = 0; i < count; i++)
            {

                if (estado[i] != 0)
                {
                    int var = estado[i];
                    if (HabitacionesCapaNegocio.modificarEstado(new Habitacion(codigoHabitacion, var)) > 0)
                    {
                        ViewBag.Mensaje = "El estado ha sido modificado";
                    }
                    else
                    {
                        ViewBag.Mensaje = "El estado no se ha modificado";
                    }
                }
            }

            ViewData["tipoHabitacion"] = HabitacionesCapaNegocio.listadoTipoHabitaciones();
            return View("modificarHabitaciones", HabitacionesCapaNegocio.listandoHabitacionTipoHabitacion(new Habitacion(idTipoHa)));

        }

        [Authorize(Roles = "Admin")]
        public ActionResult adm_descipcionhabitaciones()
        {
            return View();
        }


        public ActionResult disponibilidad_habitaciones()
        {
            return View(HabitacionesCapaNegocio.listadoTipoHabitaciones());

        }

        public ActionResult MostrarDisponibles(Reservacion reservacion)
        {
            return View(HabitacionesCapaNegocio.listarDisponibilidadHabitacion(reservacion));
        }
    }




}