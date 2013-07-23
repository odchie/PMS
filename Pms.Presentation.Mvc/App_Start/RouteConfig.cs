using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Pms.Presentation.Mvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("elmah.axd");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Login Route
            routes.MapRoute(
                "LogIn", // Route name
                "Account/{action}", // URL with parameters
                new { controller = "Account", action = "Login" } // Parameter defaults
            );

            //Localization Route
            routes.MapRoute(
                "Localization", 
                "{language}/{controller}/{action}/{id}",
                new { language = "en-US", controller = "Home", action = "Index", id = UrlParameter.Optional }                
            );

            //Default Route
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            
        }
    }
}