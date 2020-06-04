using ENTIDAD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelPlayaLinda.Controllers
{
    public class ItinerarioController : Controller
    {
        // GET: Itinerario
        NEGOCIO.ItinerarioCapaNegocio itinerario = new NEGOCIO.ItinerarioCapaNegocio();
        public ActionResult Itinerario()
        {
            return View(itinerario.listadoItinerario());
        }

        public ActionResult ItinerarioAdm()
        {
            return View(itinerario.listadoItinerario2());
        }

        public ActionResult EliminarItinerario(int id)
        {
            itinerario.eliminarItinerario(new ENTIDAD.Itinerario(id));
            return View("ItinerarioAdm", itinerario.listadoItinerario2());

        }



        [Authorize(Roles = "Admin")]
        public ActionResult RegistrarItinerario(HttpPostedFileBase fileUpload, HttpPostedFileBase fileUpload2, HttpPostedFileBase fileUpload3, string dia, string desayuno, string almuerzo, string cena)
        {

            try
            {

                string path = Server.MapPath("~/Content/img/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                fileUpload.SaveAs(path + Path.GetFileName(fileUpload.FileName));
                fileUpload2.SaveAs(path + Path.GetFileName(fileUpload2.FileName));
                fileUpload3.SaveAs(path + Path.GetFileName(fileUpload3.FileName));
                ENTIDAD.Itinerario galeria = new Itinerario();
             
                galeria.dia = dia;
                galeria.desayuno = desayuno;
                galeria.almuerzo = almuerzo;
                galeria.cena = cena;
                galeria.imgUrlDesayuno = "\\Content\\img\\" + fileUpload.FileName;
                galeria.imgUrlAlmuerzo = "\\Content\\img\\" + fileUpload2.FileName;
                galeria.imgUrlCena = "\\Content\\img\\" + fileUpload3.FileName;
                itinerario.registrarItinerario(galeria);
                return View("ItinerarioAdm", itinerario.listadoItinerario2());
            }
            catch (Exception e)
            {
                return Json(new { Value = false, Message = "Error" + e.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        [Authorize(Roles = "Admin")]
        public ActionResult ModificarItinerario(HttpPostedFileBase fileUpload, HttpPostedFileBase fileUpload2, HttpPostedFileBase fileUpload3, int idf, string dia, string desayuno, string almuerzo,string cena)
        {

            try
            {

                string path = Server.MapPath("~/Content/img/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                fileUpload.SaveAs(path + Path.GetFileName(fileUpload.FileName));
                fileUpload2.SaveAs(path + Path.GetFileName(fileUpload2.FileName));
                fileUpload3.SaveAs(path + Path.GetFileName(fileUpload3.FileName));
                ENTIDAD.Itinerario galeria = new Itinerario();
                galeria.idItinerario = idf;
                galeria.dia = dia;
                galeria.desayuno = desayuno;
                galeria.almuerzo = almuerzo;
                galeria.cena = cena;
                galeria.imgUrlDesayuno = "\\Content\\img\\" + fileUpload.FileName;
                galeria.imgUrlAlmuerzo = "\\Content\\img\\" + fileUpload2.FileName;
                galeria.imgUrlCena = "\\Content\\img\\" + fileUpload3.FileName;
                itinerario.modificarItinerario(galeria);
                return View("ItinerarioAdm", itinerario.listadoItinerario2());
            }
            catch (Exception e)
            {
                return Json(new { Value = false, Message = "Error" + e.Message }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}