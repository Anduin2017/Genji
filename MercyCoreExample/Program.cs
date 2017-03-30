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

            var server = new MercyServer();
            
            server.UseDefaultBuilder();
            server.UseDefaultReporter();
            server.UseDefaultRecorder(recordIncoming: true);
            server.UsePort(12222);

            var app = new App();

            app.UseDefaultHeaders(serverName: "Mercy", keepAlive: true);
            app.UseDefaultFile("index.html");
            app.UseStaticFile(rootPath: root);
            app.UseMvc();
            app.UseNotFound(root, "404.html");

            var condition = new AppCondition();
            
            condition.UseDomainCondition("*");

            server.Bind(when: condition, run: app);
            server.Start().Wait();
        }
    }
}