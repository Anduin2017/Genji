using Mercy.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercyCoreExample.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return $"Hello world, {HttpContext.Request.Path}";
        }

        public string About()
        {
            return "About!";
        }
    }
}
