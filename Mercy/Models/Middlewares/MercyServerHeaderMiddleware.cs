using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercy.Models.Middlewares
{
    public class ServerNameMiddleware : Middleware, IMiddleware
    {
        public string ServerName { get; set; }
        public ServerNameMiddleware(string serverName)
        {
            ServerName = serverName;
        }

        protected override void Mix(HttpContext context)
        {
            context.Response.Headers.Add("Server", ServerName);
        }

        protected override bool Excutable(HttpContext context)
        {
            return false;
        }

        protected override void Excute(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}
