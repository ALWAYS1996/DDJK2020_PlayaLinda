using ENTIDAD;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;

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

        public ActionResult DatosUsuario(string codigoTipoHabitacion, DateTime fechaLlegada, DateTime fechaSalida)
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

            //   int var= ;
                var precio = reservacionCapaNegocios.Precio(Int32.Parse(codigoTipoHabitacion));
                Reservacion reservacion = new Reservacion();
                ViewData["codigoTipoHabitacion"] = codigoTipoHabitacion;
                ViewData["codigoHabitacion"] = codigoHabitacion;
                ViewData["fechaInicio"] =fechaLlegada;
                ViewData["fechaFin"] = fechaSalida;
                fechaSalida.Subtract(fechaLlegada);
                ViewData["precio"] = precio;

                ViewData["NumeroHabitacion"] = resultado;
                ViewBag.mensaje = "Habitación disponible para ser reservada";
                return View();
            }
        }
        public ActionResult view_pdf(Cliente cliente, Reservacion reservacion,string precio) {
            ViewData["Cliente"] = cliente;
            ViewData["reservacion"] = reservacion;
            ViewData["precio"] = precio;

            return new ViewAsPdf("View_pdf"); }
        public ActionResult GenerarReservacion(int codigoHabitacion, string fechaInicio, string fechaFin, string pasaporte, string nombre, string apellido, string email, string tarjeta,string precio)
        {
            if (!email_bien_escrito(email))
            {
                ViewData["codigoHabitacion"] = codigoHabitacion;
                ViewData["fechaInicio"] = fechaInicio;
                ViewData["fechaFin"] = fechaFin;
                ViewBag.Texto = "El formato del correo no es valido";
                return View("DatosUsuario");
            }
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


  
            
            ViewData["cliente"] = cliente;
            ViewData["reservacion"] = reservacion;
            ViewData["Precio"] = precio;
            //ViewData["fechaFin"] = reservacion.fechaSalida;

            if (reservacionCapaNegocios.registrarReservacion(reservacion) == 1)
            {
                SendMail(cliente,reservacion,precio);
                return View("Reservado");
            }
            return View("Reservado");
        }



        public void SendMail(Cliente cliente,Reservacion reservacion,string precio) {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("dabrinltecr@gmail.com");
                mailMessage.To.Add(cliente.correo);
                mailMessage.Subject = "Hotel playa linda:";
                mailMessage.Body = "Usted:" + cliente.nombre + " " + cliente.apellido1 + ". Ha realizado una reservación de la habitación: " + reservacion.codigoHabitacion + 
                    " Fecha de llegada: " + reservacion.fechaLlegada + "Fecha salida: " + reservacion.fechaSalida+ " Con un precio por día de ₡" + precio;

                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com");
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential("dabrinltecr@gmail.com", "dab_Najera123");
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);
                //Response.Write("Correo Envíado");
                //HttpContext.Session.SetString("entro", "Se envió un correo de confirmación");
                //Label1.Text = "Correo Enviado";
            }
            catch (Exception ex)
            {

            }

        }


        public ActionResult Reservado() {
            return View("Reservado");

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

        private Boolean email_bien_escrito(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}