using Mercy.Library;
using Mercy.Models.Abstract;
using MercyCoreExample.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercyCoreExample.Controllers
{
    public class HomeController : Controller
    {
        private ExampleDbContext _dbContext;
        public HomeController(ExampleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return String($"Hello world, {HttpContext.Request.Path}");
        }

        public IActionResult About()
        {
            return String("About!");
        }

        public IActionResult Me()
        {
            return Json(new
            {
                id = "something",
                time = DateTime.Now,
                value = "this is my value",
                count = _dbContext.Blogs.Count()
            });
        }
    }
}
