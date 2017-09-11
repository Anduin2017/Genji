using Mercy.Library;
using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Mercy.Models.Middlewares
{
    public class DefaultFileMiddleware : Middleware, IMiddleware
    {
        public string DefaultFileName { get; set; }
        public string RootPath { get; set; }
        public string PathMatch { get; set; }
        public DefaultFileMiddleware(string rootPath, string pathMatch,string defaultFileName = "index.html")
        {
            DefaultFileName = defaultFileName;
            RootPath = rootPath;
            PathMatch = pathMatch;
        }

        protected override void Mix(HttpContext context)
        {

        }

        protected async override Task<bool> Excutable(HttpContext context)
        {
            await Task.Delay(0);
            if (context.Request.Path == PathMatch && !string.IsNullOrEmpty(DefaultFileName))
            {
                return true;
            }
            return false;
        }

        protected async override Task Excute(HttpContext context)
        {
            string contextPath = context.Request.Path.Replace('/', Path.DirectorySeparatorChar);
            string filePath = RootPath + contextPath + DefaultFileName;
            var fileExtension = Path.GetExtension(filePath).TrimStart('.');
            context.Response.ResponseCode = 200;
            context.Response.Message = "OK";
            context.Response.Headers.Add("cache-control", "max-age=3600");
            context.Response.Headers.Add("Content-Type", MIME.MIMETypesDictionary[fileExtension]);
            context.Response.Body = await Task.Run(() => File.ReadAllBytes(filePath));
        }
    }
}
