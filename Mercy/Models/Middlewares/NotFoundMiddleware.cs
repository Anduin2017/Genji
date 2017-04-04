using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Mercy.Models.Middlewares
{
    public class NotFoundMiddleware : Middleware, IMiddleware
    {
        public string Root { get; set; }
        public string ErrorPage { get; set; }
        public NotFoundMiddleware(string root, string errorPage)
        {
            Root = root;
            ErrorPage = errorPage.Replace('/', Path.DirectorySeparatorChar);
        }
        protected override void Mix(HttpContext context)
        {
        }
        protected async override Task<bool> Excutable(HttpContext context)
        {
            return true;
        }
        protected async override Task Excute(HttpContext context)
        {
            context.Response.ResponseCode = 404;
            context.Response.Message = "Not found";
            //context.Response.Body = Encoding.GetEncoding("utf-8").GetBytes("<h1>Not found!</h1>");
            context.Response.Headers.Add("Content-type", "text/html; charset=utf-8");
            if (string.IsNullOrEmpty(ErrorPage))
            {
                context.Response.Body = Encoding.GetEncoding("utf-8").GetBytes("<h1>Not found!</h1>");
            }
            else
            {
                string filePath = Root + ErrorPage;
                context.Response.Body = File.ReadAllBytes(filePath);
            }
        }
    }
}
