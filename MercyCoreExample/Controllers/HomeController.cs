using Mercy.Library;
using Mercy.Models.Abstract;
using Mercy.Models.Attributes;
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
            Response.SetCookie("MyCookie", "asdf");
            return String($"Hello world, {Request.Path}");
        }

        public IActionResult About(string id, string name)
        {
            return String("About!");
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
