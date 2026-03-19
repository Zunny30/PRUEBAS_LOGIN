
using PRUEBAS_LOGIN.Filters;
using PRUEBAS_LOGIN.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace PRUEBAS_LOGIN.Areas.Profesor.Controllers
{
    [ValidarSesion]

    [SesionActiva]  // ← protege todo el controlador
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult CerrarSesion()
        {
            Session["usuario"] = null;
            return RedirectToAction("Login", "Acceso", new { area = "Comun" });

        }
    }
}