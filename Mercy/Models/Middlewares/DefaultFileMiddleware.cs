using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mercy.Models.Middlewares
{
    public class DefaultFileMiddleware : Middleware, IMiddleware
    {
        public string DefaultFileName { get; set; }
        public DefaultFileMiddleware(string defaultFileName = "index.html")
        {
            DefaultFileName = defaultFileName;
        }

        protected override void Mix(HttpContext context)
        {
            if (context.Request.Path == "/")
            {
                context.Request.Path = "/" + DefaultFileName;
            }
        }

        protected async override Task<bool> Excutable(HttpContext context)
        {
            return false;
        }

        protected async override Task Excute(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}
