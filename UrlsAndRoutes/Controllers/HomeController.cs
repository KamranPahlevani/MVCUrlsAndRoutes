using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UrlsAndRoutes.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ViewResult CustomVariable(string id)
        {
            ///First Pattern with no id definition as method parameter
            //ViewBag.CustomVariable = RouteData.Values["id"];
            ///

            ///Second Pattern with id definition as method parameter
            ViewBag.CustomVariable = RouteData.Values["id"];
            ViewBag.CustomVariableCatchAll = RouteData.Values["catchall"];
            ///
            
            return View();
        }
    }
}
