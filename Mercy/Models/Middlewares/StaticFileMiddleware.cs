using Mercy.Library;
using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Mercy.Models.Middlewares
{
    public class StaticFileMiddleware : Middleware, IMiddleware
    {
        public string RootPath { get; set; }
        public StaticFileMiddleware(string rootPath)
        {
            RootPath = rootPath;
        }

        protected override void Mix(HttpContext context)
        {

        }

        protected async override Task<bool> Excutable(HttpContext context)
        {
            string contextPath = context.Request.Path.Replace('/', Path.DirectorySeparatorChar);
            string filePath = RootPath + contextPath;
            return await Task.Run(() => File.Exists(filePath));
        }

        protected async override Task Excute(HttpContext context)
        {
            string contextPath = context.Request.Path.Replace('/', Path.DirectorySeparatorChar);
            string filePath = RootPath + contextPath;
            var fileExtension = Path.GetExtension(filePath).TrimStart('.');
            context.Response.ResponseCode = 200;
            context.Response.Message = "OK";
            context.Response.Headers.Add("cache-control", "max-age=3600");
            context.Response.Headers.Add("Content-Type", MIME.MIMETypesDictionary[fileExtension]);
            context.Response.Body = await Task.Run(() => { File.ReadAllBytes(filePath)});
        }
    }
}
