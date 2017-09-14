using Genji.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Genji.Models.Middlewares
{
    public class HeadersMiddleware : Middleware, IMiddleware
    {
        public string ServerName { get; set; }
        public bool KeepAlive { get; set; }
        public HeadersMiddleware(string serverName = "Genji", bool keepAlive = true)
        {
            ServerName = serverName;
            KeepAlive = keepAlive;
        }

        protected override void Mix(HttpContext context)
        {
            context.Response.Headers.Add("Server", ServerName);
            context.Response.Headers.Add("Date", DateTime.Now.ToString("r"));
            if (KeepAlive)
            {
                context.Response.Headers.Add("Connection", "keep-alive");
            }
        }

        protected async override Task<bool> Excutable(HttpContext context)
        {
            await Task.Delay(0);
            return false;
        }

        protected async override Task Excute(HttpContext context)
        {
            await Task.Delay(0);
            throw new NotImplementedException();
        }
    }
}
