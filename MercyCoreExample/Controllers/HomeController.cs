using Mercy.Library;
using Mercy.Models.Abstract;
using Mercy.Service;
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
        private CleanerService _cleaner;
        private UserManager<ExampleDbContext> _userManager;

        public HomeController(
            ExampleDbContext dbContext,
            CleanerService cleaner,
            UserManager<ExampleDbContext> userManager)
        {
            _dbContext = dbContext;
            _cleaner = cleaner;
            _userManager = userManager;
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

        public IActionResult CreateBlog()
        {
            _dbContext.Blogs.Add(new Blog
            {
                Url = "asdf"
            });
            _dbContext.SaveChanges();
            return Json(new { message = "success." });
        }
    }
}
