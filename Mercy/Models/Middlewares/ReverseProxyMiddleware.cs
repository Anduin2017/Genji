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
        protected async override Task<bool> Excutable(HttpContext context)
        {
            string target = ProxyPath + context.Request.PathWithArguments;
            var http = new HTTPService();
            var result = await http.Get(target);
            return true;
        }

        protected override void Excute(HttpContext context)
        {
        }

        protected override void Mix(HttpContext context)
        {
        }
    }
}
