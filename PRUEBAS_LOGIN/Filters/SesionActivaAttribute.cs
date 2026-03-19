using System;
using System.Web.Mvc;

namespace PRUEBAS_LOGIN.Filters
{
    public class SesionActivaAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sesion = filterContext.HttpContext.Session["usuario"];

            if (sesion == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(new
                    {
                        area = "Comun",
                        controller = "Acceso",
                        action = "Login"
                    })
                );
            }

            base.OnActionExecuting(filterContext);
        }
    }
}