using Mercy.Library;
using Mercy.Models;
using Mercy.Models.Abstract;
using Mercy.Service;
using MercyCoreExample.Controllers;
using MercyCoreExample.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MercyCoreExample
{
    public class StartUp
    {
        public static ServiceGroup ConfigServices()
        {
            var services = new ServiceGroup()
                .RegisterController<HomeController>()
                .RegisterController<AccountController>();

            services
                .RegisterService<ExampleDbContext>()
                .RegisterService<UserManager<ExampleDbContext>>();

            return services;
        }

        public static IServer ConfigureServer(ServiceGroup services)
        {
            string root = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar;
            string wwwroot = root + "wwwroot";
            string viewroot = root + "Views";

            var server = new MercyServer()
                .UseDefaultSettings(logpath: root)
                .UsePort(8001);

            var app = new App()
                .UseHeaders(serverName: "Mercy", keepAlive: false)
                .UseStaticFile(rootPath: wwwroot)
                .UseRoute(sourcePath: "/", controllerName: "Home", actionName: "Index")
                .UseMvc(viewroot: viewroot, services: services)
                .UseNotFound(root: viewroot, errorPage: "/Shared/404.html");

            var condition = new AppCondition()
                .UseDomainCondition("localhost");

            server.Bind(when: condition, run: app);
            return server;
        }
    }
}
