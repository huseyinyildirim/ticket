using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("FirstGuest", "", new { controller = "Login", action = "Index" });
            routes.MapRoute("Image", "Image", new { controller = "CP", action = "ImageResize" });
            routes.MapRoute("SessionState", "SessionState", new { controller = "Session", action = "Index" });
            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
