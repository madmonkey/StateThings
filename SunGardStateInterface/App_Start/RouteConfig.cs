using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StateInterface
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                 "Default",
                 "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "StateInterface.Controllers" }
            );

            // Show a 404 page for anything else.
            routes.MapRoute("NotFound", "{*url}",
                new { controller = "Home", action = "NotFound" },
                new[] { "StateInterface.Controllers" }

            );
        }
    }
}