using System;
using System.IO;
using Mercy.Library;
using Mercy.Models;
using Mercy.Models.Middlewares;

namespace MercyCoreExample
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            string root = $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}wwwroot";

            var server = new MercyServer()
                .UseDefaultBuilder()
                .UseDefaultReporter()
                .UseDefaultRecorder(recordIncoming: false)
                .UsePort(9000);

            var app = new App()
                .UseDefaultHeaders(serverName: "Mercy", keepAlive: true)
                .UseDefaultFile("index.html")
                .UseStaticFile(rootPath: root)
                .UseMvc()
                .UseNotFound(root, errorPage: "/404.html");

            var condition = new AppCondition()
                .UseDomainCondition("localhost");

            server.Bind(when: condition, run: app);
            server.Start().Wait();
        }
    }
}