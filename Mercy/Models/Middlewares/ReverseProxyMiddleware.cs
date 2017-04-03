using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Mercy.Library;
using System.Threading.Tasks;

namespace Mercy.Models.Middlewares
{
    public class ReverseProxyMiddleware : Middleware, IMiddleware
    {
        public string ProxyPath { get; set; }
        public ReverseProxyMiddleware(string proxyPath)
        {
            this.ProxyPath = proxyPath;
        }

        private string _RightNowResult { get; set; }

        protected async override Task<bool> Excutable(HttpContext context)
        {
            string target = ProxyPath + context.Request.PathWithArguments;
            var http = new HTTPService();
            try
            {
                _RightNowResult = await http.Get(target);
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected override void Excute(HttpContext context)
        {
            context.Response.ResponseCode = 200;
            context.Response.Message = "OK";
            context.Response.Headers.Add("Content-type", "text/html; charset=utf-8");
            context.Response.Body = Encoding.GetEncoding("utf-8").GetBytes(_RightNowResult);
        }

        protected override void Mix(HttpContext context)
        {
        }
    }
}
