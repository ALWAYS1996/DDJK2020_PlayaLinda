using HotelPlayaLinda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HotelPlayaLinda.Controllers
{
    public class AdministradorController : Controller
    {
        [Authorize(Roles = "Admin")]
        // GET: Administrador
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Administrador");
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login model, string returnUrl)
        {
            DDJKEntities db = new DDJKEntities();
            try
            {
                var dataItem = db.Login.Where(x => x.Username == model.Username && x.Password == model.Password).First();
              //  int con = int.Parse(dataItem.ToString());
                if (dataItem != null)
                {
                    FormsAuthentication.SetAuthCookie(dataItem.Username, false);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid user/pass");
                        return Redirect("Index");
                    }
                }

                else
                {
                    ModelState.AddModelError("", "Invalid user/pass");
                    return View();
                }
                

            }
            catch (Exception) {
                ModelState.AddModelError("", "Invalido Usuario/contraseña");
            }
            return View();
        }

    }
}