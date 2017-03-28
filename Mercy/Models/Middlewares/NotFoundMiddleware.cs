using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Mercy.Models.Middlewares
{
    public class NotFoundMiddleware : Middleware, IMiddleware
    {
        public string Root { get; set; }
        public string Page { get; set; }
        public NotFoundMiddleware(string root,string page)
        {
            Root = root;
            Page = page;
        }
        protected override void Mix(HttpContext context)
        {
        }
        protected override bool Excutable(HttpContext context)
        {
            return true;
        }
        protected override void Excute(HttpContext context)
        {
            string filePath = Root + Path.DirectorySeparatorChar + Page;
            context.Response.ResponseCode = 404;
            context.Response.Message = "Not found";
            context.Response.Body = Encoding.GetEncoding("utf-8").GetBytes("<h1>Not found!</h1>");
            context.Response.Headers.Add("Content-type", "text/html; charset=utf-8");
            context.Response.Body = File.ReadAllBytes(filePath);
        }
    }
}
