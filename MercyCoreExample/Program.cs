using System;
using System.IO;
using Mercy.Library;
using Mercy.Models;

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
                .UseDefaultRecorder(recordIncoming: true)
                .UsePort(12222);

            var app = new App()
                .UseDefaultHeaders(serverName: "Mercy", keepAlive: true)
                .UseDefaultFile("index.html")
                .UseStaticFile(rootPath: root)
                .UseMvc()
                .UseNotFound(root, "/views/404.html");

            var condition = new AppCondition()
                .UseDomainCondition("*");

            server.Bind(when: condition, run: app);
            server.Start().Wait();
        }
    }
}