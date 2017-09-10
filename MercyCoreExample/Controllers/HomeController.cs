using Mercy.Library;
using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercyCoreExample.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return String($"Hello world, {HttpContext.Request.Path}");
        }

        public IActionResult About()
        {
            return String("About!");
        }
    }
}
