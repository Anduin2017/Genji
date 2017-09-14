using Genji.Library;
using Genji.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genji.Models.Attributes;

namespace GenjiExample.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return String("This is Account page.");
        }

        [HttpGet]
        public IActionResult SignIn(string path)
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn()
        {
            return String("Successfully signed in.");
        }
    }
}
