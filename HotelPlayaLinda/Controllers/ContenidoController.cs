using ENTIDAD;
using System;
using System.Collections.Generic;
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
        public ActionResult Contacto()
        {

            ViewData["contenidoVista"] = capaNegocios.listadoContenido(new ENTIDAD.Contenido(2));
            return View();
        }


        public ActionResult Nosotros()
        {

            ViewData["contenidoVista"] = capaNegocios.listadoContenido(new ENTIDAD.Contenido(1));
            return View(img.listadoImagenes(new ENTIDAD.Imagen(1)));
        }


        public ActionResult Inicio()
        {

            ViewData["listadoPromociones"] = promocionCapaNegocio.listadoPromociones();
            ViewData["contenidoVista"] = capaNegocios.listadoContenido(new ENTIDAD.Contenido(4));
            return View(img.listadoImagenes(new ENTIDAD.Imagen(2)));
        }

        public ActionResult Administrar_ofertas() {
            ViewData["listadoPromociones"] = promocionCapaNegocio.listadoPromociones();
            return View();
        }
        public ActionResult Crear_oferta(string informacion,DateTime fechaInicio,DateTime fechaFinal,int precio)
        {
            Promocion promocion = new Promocion();
            promocion.precio = precio;
            promocion.informacion = informacion;
            promocion.fechaInicio = fechaInicio;
            promocion.fechaFinal = fechaFinal;

            promocionCapaNegocio.createPromo(promocion);
            ViewData["listadoPromociones"] = promocionCapaNegocio.listadoPromociones();
            return View("Administrar_ofertas");
        }

        public ActionResult Eliminar_oferta(int codigoPromocion)
        {
            

            promocionCapaNegocio.deletePromo(codigoPromocion);
            ViewData["listadoPromociones"] = promocionCapaNegocio.listadoPromociones();
            return View("Administrar_ofertas");
        }

        public ActionResult obtenerPromoById(int codigoPromocion)
        {
            //promocionCapaNegocio.deletePromo(codigoPromocion);
            ViewData["promo"] = promocionCapaNegocio.obtenerPromoById(codigoPromocion);
            return View("ObtenerPromo");
        }

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
    }
}