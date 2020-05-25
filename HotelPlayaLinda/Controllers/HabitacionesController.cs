using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ENTIDAD;
using NEGOCIO;

namespace HotelPlayaLinda.Controllers
{
    public class HabitacionesController : Controller
    {
        NEGOCIO.HabitacionCapaNegocio HabitacionesCapaNegocio = new NEGOCIO.HabitacionCapaNegocio();


        [Authorize(Roles = "Admin")]
        public ActionResult adm_estadohoy()
        {
        return View(HabitacionesCapaNegocio.listarEstadoHoyHabitacion());
        }
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
        public ActionResult adm_descipcionhabitaciones(int idTipoHab)
        {
            return View(HabitacionesCapaNegocio.filtrandoTipoHabitaciones(new TipoHabitacion(idTipoHab)));
        }


        public ActionResult disponibilidad_habitaciones()
        {
            return View(HabitacionesCapaNegocio.listadoTipoHabitaciones());

        }
        public ActionResult modificarTipoHabitacion(int codigoTipoHabitacion, string nombre, int precio, string descripcion) {

        if(HabitacionesCapaNegocio.modificarTipoHabitacion(new TipoHabitacion(codigoTipoHabitacion, nombre, precio, descripcion))>0){
                ViewBag.mensaje = "Se ha modificado correctamente";
            }else{
                ViewBag.mensaje = "Imporsible modificar";
            }
            return View("adm_descipcionhabitaciones", HabitacionesCapaNegocio.filtrandoTipoHabitaciones(new TipoHabitacion(codigoTipoHabitacion)));
        

    }
    public ActionResult modificarImagenTipoHabitacion(HttpPostedFileBase fileUpload, int cod)
        {
            try
            {
                string path = Server.MapPath("~/Content/img/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                 fileUpload.SaveAs(path + Path.GetFileName(fileUpload.FileName));
                ENTIDAD.TipoHabitacion galeria = new ENTIDAD.TipoHabitacion();
                galeria.urlImg = "\\Content\\img\\" + fileUpload.FileName;
                galeria.codigoTipoHabitacion = cod;
              //  img.modificarImagenes(galeria);
                if (HabitacionesCapaNegocio.modificarImagenTipoHabitacion(galeria) > 0)
                {
                    ViewBag.mensaje = "Se ha modificado Exitosamente";
                }
                else
                {
                    ViewBag.mensaje = "No ha sido posible modificar la imagen";
                }
             
            }
            catch (Exception e)
            {
                return Json(new { Value = false, Mensaje = "Error" + e.Message }, JsonRequestBehavior.AllowGet);
            }
            ViewBag.mensaje = "Se ha modificado Exitosamente";
            return Json(new { Value = false, mensaje = "Subido con exito" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MostrarDisponibles(Reservacion reservacion)
        {
            return View(HabitacionesCapaNegocio.listarDisponibilidadHabitacion(reservacion));
        }
    }




}