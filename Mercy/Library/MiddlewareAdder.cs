using Mercy.Models;
using Mercy.Models.Abstract;
using Mercy.Models.Conditions;
using Mercy.Models.Middlewares;
using Mercy.Models.Workers;
using Mercy.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercy.Library
{
    public static class MiddlewareAdder
    {
        public static IMiddleware UseHeaders(this IMiddleware host, string serverName = "Mercy", bool keepAlive = true)
        {
            return host.InsertMiddleware(new HeadersMiddleware(serverName, keepAlive));
        }

        public static IMiddleware UseDefaultFile(this IMiddleware host, string location, string pathMatch, string fileName = "index.html")
        {
            return host.InsertMiddleware(new DefaultFileMiddleware(location, pathMatch, fileName));
        }

        public static IMiddleware UseStaticFile(this IMiddleware host, string rootPath)
        {
            return host.InsertMiddleware(new StaticFileMiddleware(rootPath));
        }

        public static IMiddleware UseRoute(this IMiddleware host, string sourcePath, string controllerName, string actionName)
        {
            return host.InsertMiddleware(new RouteMiddleware(sourcePath, controllerName, actionName));
        }

        public static IMiddleware UseMvc(this IMiddleware host, string viewroot, ServiceGroup services, bool checkPathCase = false)
        {
            return host.InsertMiddleware(new MvcMiddleware(viewroot, services, checkPathCase));
        }

        public static IMiddleware UseNotFound(this IMiddleware host, string root, string errorPage)
        {
            return host.InsertMiddleware(new NotFoundMiddleware(root, errorPage));
        }

        public static IServer UseDefaultBuilder(this IServer server)
        {
            return server.UseBuilder(new HttpBuilder());
        }
        public static IServer UseDefaultReporter(this IServer server)
        {
            return server.UseReporter(new HttpReporter());
        }
        public static IServer UseDefaultRecorder(this IServer server, bool recordIncoming = false)
        {
            return server.UseRecorder(new HttpRecorder(recordIncoming));
        }
        public static IServer UseDefaultSettings(this IServer server)
        {
            return server.UseDefaultBuilder().UseDefaultReporter().UseDefaultRecorder(false);
        }

        public static ICondition UseDomainCondition(this ICondition condition, string domain)
        {
            return condition.InsertCondition(new DomainCondition(domain));
        }
    }
}
