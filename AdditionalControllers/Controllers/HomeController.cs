using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AdditionalControllers.Controllers
{
    public class HomeController : Controller 
    {
        public string Index()
        {
            return "Additional Controller";
        }
    }
}
