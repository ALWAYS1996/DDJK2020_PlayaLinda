using ENTIDAD;

using System.Web.Mvc;

namespace HotelPlayaLinda.Controllers
{
    public class MapaController : Controller
    {
        // GET: Mapa

        NEGOCIO.MapaCapaNegocio capaNegocios = new NEGOCIO.MapaCapaNegocio();
        NEGOCIO.ContenidoCapaNegocio contenido = new NEGOCIO.ContenidoCapaNegocio();
        public ViewResult Mapa(string lat, string lng)
        {
            ENTIDAD.Mapa mapa = new ENTIDAD.Mapa(); mapa.latitudOrigen = lat; mapa.longitudOrigen = lng;
            ViewData["contenidoVista"] = contenido.listadoContenido(new ENTIDAD.Contenido(3));
            capaNegocios.modificarCoordenadasOrigen(mapa);
            return View(capaNegocios.listadoCoordenadasOrigen());

        }
        [Authorize(Roles = "Admin")]
        public ActionResult AdministrarContenido()
        {
            return View(contenido.listadoContenido(new ENTIDAD.Contenido(3)));
        }
        [Authorize(Roles = "Admin")]

        public ActionResult ModificarContenido(string contenido1, string titulo) {
            if (contenido.modificarContenido(new Contenido(3, contenido1, titulo)) > 0)
            {
                ViewBag.mensaje = "Se ha modificado correctamente";
            }
            else {
                ViewBag.mensaje = "Lo sentimos. No ha sido posible modificarlo";
            }
            return View("AdministrarContenido", contenido.listadoContenido(new ENTIDAD.Contenido(3)));
        }
        public ActionResult Llegar()
        {
            return View();
        }
    }

}