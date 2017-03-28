using Mercy.Models.Abstract;
using Mercy.Models.Middlewares;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercy.Library
{
    public static class MiddlewareAdder
    {
        public static IMiddleware UseDefaultHeaders(this IMiddleware host, string serverName = "Mercy", bool keepAlive = true)
        {
            return host.InsertMiddleware(new DefaultHeadersMiddleware(serverName, keepAlive));
        }

        public static IMiddleware UseDefaultFile(this IMiddleware host, string defaultFileName = "index.html")
        {
            return host.InsertMiddleware(new DefaultFileMiddleware(defaultFileName));
        }

        public static IMiddleware UseStaticFile(this IMiddleware host, string rootPath)
        {
            return host.InsertMiddleware(new StaticFileMiddleware(rootPath));
        }

        public static IMiddleware UseMvc(this IMiddleware host, string route)
        {
            return host.InsertMiddleware(new MvcMiddleware(route));
        }

        public static IMiddleware UseNotFound(this IMiddleware host, string root, string page)
        {
            return host.InsertMiddleware(new NotFoundMiddleware(root, page));
        }
    }
}
