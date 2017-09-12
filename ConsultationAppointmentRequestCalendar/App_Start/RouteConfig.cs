using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ConsultationAppointmentRequestCalendar
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Physician",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Physician", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Appointment",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Appointment", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
