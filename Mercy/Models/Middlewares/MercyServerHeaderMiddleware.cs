using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mercy.Models.Middlewares
{
    public class DefaultHeadersMiddleware : Middleware, IMiddleware
    {
        public string ServerName { get; set; }
        public bool KeepAlive { get; set; }
        public DefaultHeadersMiddleware(string serverName = "Mercy", bool keepAlive = true)
        {
            ServerName = serverName;
            KeepAlive = keepAlive;
        }

        protected override void Mix(HttpContext context)
        {
            context.Response.Headers.Add("Server", ServerName);
            if (KeepAlive)
            {
                context.Response.Headers.Add("Connection", "keep-alive");
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
