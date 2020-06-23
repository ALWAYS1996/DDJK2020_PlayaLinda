using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace HotelPlayaLinda.Controllers
{
    public class ContenidoController : Controller
    {
        // GET: Contenido
        NEGOCIO.ContenidoCapaNegocio capaNegocios = new NEGOCIO.ContenidoCapaNegocio();
        NEGOCIO.ImagenCapaNegocio img = new NEGOCIO.ImagenCapaNegocio();
        NEGOCIO.PromocionCapaNegocio promocionCapaNegocio = new NEGOCIO.PromocionCapaNegocio();
        NEGOCIO.FacilidadesCapaNegocio facilidades = new NEGOCIO.FacilidadesCapaNegocio();
        NEGOCIO.PublicidadCapaNegocio publicidad = new NEGOCIO.PublicidadCapaNegocio();
        public ActionResult Contacto()
        {
            ViewData["contenidoVista"] = capaNegocios.listadoContenido(new ENTIDAD.Contenido(2));
            return View();
        }


        public JsonResult listadoPublicidad()
        {

            return Json((publicidad.listadoPublicidad()));

        }
        [Authorize(Roles = "Admin")]
        public ActionResult Administrar_Publicidad() {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Administrar_Inicio()
        {
            return View();
        }

        
        public ActionResult Nosotros()
        {

            ViewData["contenidoVista"] = capaNegocios.listadoContenido(new ENTIDAD.Contenido(1));
            return View(img.listadoImagenes(new ENTIDAD.Imagen(1)));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdministrarPaginas() {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult SobreNosotrosAdm() {
         
            ViewData["contenidoVista"] = capaNegocios.listadoContenido(new ENTIDAD.Contenido(1));
            return View(img.listadoImagenes(new ENTIDAD.Imagen(1)));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult FacilidadesAdm()
        {  
            return View(facilidades.listadoFacilidades2());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult EliminarFacilidad(int id)
        {
            facilidades.eliminarFacilidades(new Facilidades(id));
            return View("FacilidadesAdm", facilidades.listadoFacilidades2());
        }

        

        [Authorize(Roles = "Admin")]
        public ActionResult RegistrarFacilidades(HttpPostedFileBase fileUpload, string nombre, string regla, string detalle)
        {
            try
            {

                string path = Server.MapPath("~/Content/img/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                fileUpload.SaveAs(path + Path.GetFileName(fileUpload.FileName));
                Facilidades facilidad = new Facilidades();
                facilidad.nombre = nombre;
                facilidad.reglas = regla;
                facilidad.detalles = detalle;
                facilidad.urlImg = "\\Content\\img\\" + fileUpload.FileName;
                facilidades.registrarFacilidades(facilidad);
                return View("FacilidadesAdm", facilidades.listadoFacilidades2());
            }catch (Exception e)
            {
                return Json(new { Value = false, Message = "Error" + e.Message }, JsonRequestBehavior.AllowGet);
            }

        }






        [Authorize(Roles = "Admin")]
        public ActionResult HomeAdm()
        {
            ViewData["contenidoInicio"] = capaNegocios.listadoContenido(new ENTIDAD.Contenido(4));
            ViewData["contenidoImagen"] = img.listadoImagenes(new ENTIDAD.Imagen(2));
            return View("Administrar_Home");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ModificarFacilidades(HttpPostedFileBase fileUpload,int idf,string nombre, string regla,string detalle, string img)
        {

            try
            {

                string path = Server.MapPath("~/Content/img/");
                if (fileUpload != null ) {

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    fileUpload.SaveAs(path + Path.GetFileName(fileUpload.FileName));

                }
               
                ENTIDAD.Facilidades galeria = new Facilidades();
                galeria.id_Facilidades = idf;
                galeria.nombre = nombre;
                galeria.reglas = regla;
                galeria.detalles = detalle;

                if (fileUpload != null)
                {

                    galeria.urlImg = "\\Content\\img\\" + fileUpload.FileName;

                }
                else 
                {

                    galeria.urlImg = "\\Content\\img\\" + img;

                }

                facilidades.modificarFacilidades(galeria);
                return View("FacilidadesAdm", facilidades.listadoFacilidades2());
            }
            catch (Exception e)
            {
                return Json(new { Value = false, Message = "Error" + e.Message }, JsonRequestBehavior.AllowGet);
            }

        }


        [Authorize(Roles = "Admin")]
        public ActionResult ModificarContenido(string contenido, string titulo) {
            if (capaNegocios.modificarContenido(new Contenido(1, contenido, titulo)) > 0)
            {
                ViewBag.mensaje = "Modificado Correctamente";
            }
            else
            {
                ViewBag.mensaje = "No Modificado Correctamente";
            }
                

              //  capaNegocios.modificarContenido(new Contenido(1, contenido, titulo));
            ViewData["contenidoVista"] = capaNegocios.listadoContenido(new ENTIDAD.Contenido(1));
            return View("SobreNosotrosAdm", img.listadoImagenes(new ENTIDAD.Imagen(1)));
        }

        public ActionResult ModificarTextoInicio(string contenido, string titulo)
        {
            capaNegocios.modificarContenido(new Contenido(4, contenido, titulo));
            ViewData["contenidoInicio"] = capaNegocios.listadoContenido(new ENTIDAD.Contenido(4));
            ViewData["contenidoImagen"] = img.listadoImagenes(new ENTIDAD.Imagen(2));
            return View("Administrar_Home");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ModificarImagen(HttpPostedFileBase fileUpload, int idImagen)
        {
            try
            {
                string path = Server.MapPath("~/Content/img/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                fileUpload.SaveAs(path + Path.GetFileName(fileUpload.FileName));
                ENTIDAD.Imagen galeria = new ENTIDAD.Imagen();
                galeria.idImagen = idImagen;
                galeria.imgPath = "\\Content\\img\\" + fileUpload.FileName;

                img.modificarImagenes(galeria);
                ViewData["contenidoVista"] = capaNegocios.listadoContenido(new ENTIDAD.Contenido(1));
                return View("SobreNosotrosAdm", img.listadoImagenes(new ENTIDAD.Imagen(1)));
            }
            catch (Exception e)
            {
                return Json(new { Value = false, Message = "Error" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        
               [Authorize(Roles = "Admin")]
        public ActionResult RegistrarSobreNosotros(HttpPostedFileBase fileUpload1)
        {
            try
            {
                string path = Server.MapPath("~/Content/img/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                fileUpload1.SaveAs(path + Path.GetFileName(fileUpload1.FileName));
                ENTIDAD.Imagen galeria = new ENTIDAD.Imagen();
        
                galeria.imgPath = "\\Content\\img\\" + fileUpload1.FileName;
                galeria.tipo = 1;
                img.registrarImagenes(galeria);
               
                ViewData["contenidoVista"] = capaNegocios.listadoContenido(new ENTIDAD.Contenido(1));
                return View("SobreNosotrosAdm", img.listadoImagenes(new ENTIDAD.Imagen(1)));
            }
            catch (Exception e)
            {
                return Json(new { Value = false, Message = "Error" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult eliminarSobreNosotros(int idImagen) {
            ENTIDAD.Imagen galeria = new ENTIDAD.Imagen();
            galeria.idImagen = idImagen;
            img.eliminarImagenes(galeria);
            ViewData["contenidoVista"] = capaNegocios.listadoContenido(new ENTIDAD.Contenido(1));
            return View("SobreNosotrosAdm", img.listadoImagenes(new ENTIDAD.Imagen(1)));
        }


        public ActionResult ModificarImagenHome(int idImagen, Imagen img) {
            try
            {
                Imagen image = new Imagen();
                string filename = Path.GetFileNameWithoutExtension(img.ImageFile.FileName);
                string extension = Path.GetExtension(img.ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;

                img.idImagen = idImagen;
                img.imgPath= "\\Content\\img\\" + filename;

                filename = Path.Combine(Server.MapPath("\\Content\\img\\"), filename);
                img.ImageFile.SaveAs(filename);

                this.img.modificarImagenes(img);
               ViewData["contenidoInicio"] = capaNegocios.listadoContenido(new ENTIDAD.Contenido(4));
                //return View("Administrar_Home", img.listadoImagenes(new ENTIDAD.Imagen(2)));

                ViewData["contenidoImagen"] = this.img.listadoImagenes(new ENTIDAD.Imagen(2));
                return View("Administrar_Home");
            }
            catch (Exception e)
            {
                return Json(new { Value = false, Message = "Error" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

      
        public ActionResult Inicio()
        {

            ViewData["listadoPromociones"] = promocionCapaNegocio.listadoPromociones();
            ViewData["contenidoVista"] = capaNegocios.listadoContenido(new ENTIDAD.Contenido(4));
            
           
            return View(img.listadoImagenes(new ENTIDAD.Imagen(2)));
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Administrar_ofertas() {
            ViewData["listadoPromociones"] = promocionCapaNegocio.listadoPromociones();
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Crear_oferta(string informacion,DateTime fechaInicio,DateTime fechaFinal,int precio, Imagen img)
        {
            Promocion promocion = new Promocion();

            string filename = Path.GetFileNameWithoutExtension(img.ImageFile.FileName);
            string extension = Path.GetExtension(img.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;

            promocion.precio = precio;
            promocion.imgUrl= "/Content/img/"+filename;
            promocion.informacion = informacion;
            promocion.fechaInicio = fechaInicio;
            promocion.fechaFinal = fechaFinal;

            filename = Path.Combine(Server.MapPath("~/Content/img/"), filename);
            img.ImageFile.SaveAs(filename);
            

            promocionCapaNegocio.createPromo(promocion);
            ViewData["listadoPromociones"] = promocionCapaNegocio.listadoPromociones();
            return View("Administrar_ofertas");
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Eliminar_oferta(int codigoPromocion)
        {
            

            promocionCapaNegocio.deletePromo(codigoPromocion);
            ViewData["listadoPromociones"] = promocionCapaNegocio.listadoPromociones();
            return View("Administrar_ofertas");
        }


        [Authorize(Roles = "Admin")]
        public ActionResult obtenerPromoById(int codigoPromocion)
        {
            //promocionCapaNegocio.deletePromo(codigoPromocion);
            ViewData["promo"] = promocionCapaNegocio.obtenerPromoById(codigoPromocion);
            return View("ObtenerPromo");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult actualizarPromo(int codigoPromocion,string informacion, DateTime fechaInicio, DateTime fechaFinal, int precio) {

            Promocion promocion = new Promocion();
            promocion.codigoPromocion = codigoPromocion;
            promocion.precio = precio;
            promocion.informacion = informacion;
            promocion.fechaInicio = fechaInicio;
            promocion.fechaFinal = fechaFinal;

            promocionCapaNegocio.updatePromo(promocion);
            ViewData["listadoPromociones"] = promocionCapaNegocio.listadoPromociones();
            return View("Administrar_ofertas");
        }



        public string uploadImag(HttpPostedFile file) {
            try { if (file.ContentLength > 0) {
                    string filename = Path.GetFileName(file.FileName);
                    string filepath = Path.Combine(Server.MapPath("/img"),filename);
                    file.SaveAs(filepath);
                    return filepath;
                } } catch { return "error"; }
            return "Error";
    
            
        }
    }
}