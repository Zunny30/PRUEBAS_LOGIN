using System.Web.Mvc;

namespace PRUEBAS_LOGIN.Areas.Comun
{
    public class ComunAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Comun";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Comun_default",
                "Comun/{controller}/{action}/{id}",
                new
                {
                    controller = "Acceso",      // ← AGREGA ESTO
                    action = "Login",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}