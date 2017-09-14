using Genji.Library;
using Genji.Models;
using Genji.Models.Abstract;
using Genji.Service;
using GenjiExample.Controllers;
using GenjiExample.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GenjiExample
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

            var server = new GenjiServer()
                .UseDefaultSettings(logpath: root)
                .UsePort(8001);

            var app = new App()
                .UseHeaders(serverName: "Genji", keepAlive: false)
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
