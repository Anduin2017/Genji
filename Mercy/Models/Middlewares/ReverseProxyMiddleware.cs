using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Mercy.Library;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Mercy.Models.Middlewares
{
    public class ReverseProxyMiddleware : Middleware, IMiddleware
    {
        public string ProxyPath { get; set; }
        public ReverseProxyMiddleware(string proxyPath)
        {
            this.ProxyPath = proxyPath;
        }

        public static HTTPService http { get; set; } = new HTTPService();

        protected override Task<bool> Excutable(HttpContext context)
        {
            throw new NotImplementedException();
        }

        protected override Task Excute(HttpContext context)
        {
            throw new NotImplementedException();
        }

        protected override void Mix(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}
