using Genji.Library;
using Genji.Models.Abstract;
using Genji.Models.Attributes;
using Genji.Service;
using GenjiExample.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenjiExample.Controllers
{
    public class HomeController : Controller
    {
        private ExampleDbContext _dbContext;
        private UserManager<ExampleDbContext> _userManager;

        public HomeController(
            ExampleDbContext dbContext,
            UserManager<ExampleDbContext> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About(string id, string name)
        {
            return String("About!");
        }

        [HttpGet]
        public IActionResult Time()
        {
            return Json(DateTime.Now);
        }

        [HttpGet]
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

        [HttpPost]
        public async Task<IActionResult> CreateBlog()
        {
            _dbContext.Blogs.Add(new Blog
            {
                Url = "asdf"
            });
            await _dbContext.SaveChangesAsync();
            return Json(new { message = "success." });
        }
    }
}
