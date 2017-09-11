using Mercy.Library;
using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercyCoreExample.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return String("This is Account page.");
        }
        public IActionResult SignIn()
        {
            return View();
        }
    }
}
