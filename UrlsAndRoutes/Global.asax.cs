using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UrlsAndRoutes.CustomRouteConstraints;
using UrlsAndRoutes.Infrastructure;
using UrlsAndRoutes.Infrastructure.CustomRoute;

namespace UrlsAndRoutes
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            /// To enabling routing for static files
            routes.RouteExistingFiles = true;

            /// Ignoring routes in MVC will tell the MVC framework not to pick up those URLs.
            /// This means that it will let the underlying ASP.NET handle the request, which will happily show you a static file
            routes.IgnoreRoute("Content/{filename}.css");

            /// Custom Route Handler
            //routes.Add(new Route("SayHello", new CustomRouteHandler()));

            ///Create Custom Route for Input Address
            routes.Add(new LegacyRoute("~/articles/Windows_3.1_Overview.html",
                                       "~/old/.Net_1.0_Class_Library"));

            /// First Pattern
            Route myRoute = new Route("{controller}/{action}", new MvcRouteHandler());
            routes.Add("MyRoute0", myRoute);
            

            /// Second Pattern            
            routes.MapRoute("", "Public/{controller}/{action}",
                            new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                            new[] { "UrlsAndRoutes.Controllers" });
            
            routes.MapRoute("", "X{controller}/{action}", 
                            new { controller = "Home", action = "Index" },
                            new[] { "UrlsAndRoutes.Controllers" });

            routes.MapRoute("ShopSchema2", "Shop/OldAction", 
                            new { controller = "Home", action = "Index" },
                            new[] { "UrlsAndRoutes.Controllers" });

            routes.MapRoute("ShopSchema", "Shop/{action}", 
                            new { controller = "Home", action = "Index" },
                            new[] { "UrlsAndRoutes.Controllers" });

            routes.MapRoute("DiskFile", "Content/StaticContent.html",
                            new { controller = "Account", action = "LogOn" },
                            new { customConstraint = new UserAgentConstraint("Chrome") });

            routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
                            new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                            new {controller="^H.*",
                                 action="^Index$|^About$", 
                                 httpMethod=new HttpMethodConstraint("GET","POST"),
                                 customConstraint = new UserAgentConstraint("IE")},
                            new[] { "UrlsAndRoutes.Controllers" });

            Route AdditionalRoute = routes.MapRoute("MyAdditionalRoute", "{controller}/{action}/{id}/{*catchall}", 
                                                    new { controller = "AdditionalHome", action = "Index", id = UrlParameter.Optional }, 
                                                    new[] { "AdditionalControllers.Controllers" });
            AdditionalRoute.DataTokens["UseNamespaceFallback"] = false;

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}