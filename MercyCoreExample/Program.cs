using System;
using System.IO;
using Mercy.Library;
using Mercy.Models;
using Mercy.Models.Middlewares;
using Mercy.Service;
using MercyCoreExample.Data;
using MercyCoreExample.Controllers;

namespace MercyCoreExample
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            string root = $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}";
            string wwwroot = $"{root}wwwroot";

            var server = new MercyServer()
                .UseDefaultBuilder()
                .UseDefaultReporter()
                .UseDefaultRecorder(recordIncoming: false)
                .UsePort(8001);

            var app = new App()
                .UseDefaultHeaders(serverName: "Mercy", keepAlive: false)
                .UseDefaultFile(rootPath: wwwroot, defaultFileName: "index.html")
                .UseStaticFile(rootPath: wwwroot)
                .UseMvc(services: ConfigServices())
                .UseNotFound(root: root, errorPage: "/Views/Shared/404.html");

            var condition = new AppCondition()
                .UseDomainCondition("localhost");

            server.Bind(when: condition, run: app);
            server.Start().Wait();
        }

        public static ServiceGroup ConfigServices()
        {
            ExampleDbContext.ConnectionString = "Data Source=blogging.db";

            var services = new ServiceGroup()
                .RegisterController<HomeController>()
                .RegisterController<AccountController>();

            services
                .RegisterService<ExampleDbContext>()
                .RegisterService<UserManager<ExampleDbContext>>()
                .RegisterService<CleanerService>();

            return services;
        }
    }
}