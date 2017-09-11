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
            Console.WriteLine("Starting Mercy server....");
            string root = $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}wwwroot";

            var server = new MercyServer()
                .UseDefaultBuilder()
                .UseDefaultReporter()
                .UseDefaultRecorder(recordIncoming: false)
                .UsePort(8001);

            var services = new ServiceGroup()
                .RegisterController<HomeController>()
                .Register<ExampleDbContext>()
                .Register<UserManager<ExampleDbContext>>()
                .Register<CleanerService>();

            var app = new App()
                .UseDefaultHeaders(serverName: "Mercy", keepAlive: true)
                .UseDefaultFile(rootPath: root, defaultFileName: "index.html")
                .UseStaticFile(rootPath: root)
                .UseMvc(services: services)
                .UseNotFound(root, errorPage: "/404.html");

            var condition = new AppCondition()
                .UseDomainCondition("localhost");

            server.Bind(when: condition, run: app);
            server.Start().Wait();
        }
    }
}