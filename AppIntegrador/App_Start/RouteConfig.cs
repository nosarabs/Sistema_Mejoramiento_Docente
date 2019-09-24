﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AppIntegrador
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "estadisticas",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "PlanMejoramiento", action = "estadisticas", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "planMejoramiento",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "PlanMejoramiento", action = "planMejoramiento", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "About",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "About", id = UrlParameter.Optional }
            );
        }
    }
}
