using System;
using System.IO;
using Mercy.Models.Workers;
using Mercy.Models.Server;
using Mercy.Models.Conditions;
using Mercy.Models.Middlewares;
using Mercy.Library;

namespace Mercy
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            string root = $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}wwwroot";

            var server = new MercyServer()
                .UseBuilder(new HttpBuilder())
                .UseReporter(new HttpReporter())
                .UseRecorder(new HttpRecorder(recordingIncoming: false))
                .UsePort(9000);

            var app = new App()
                .UseDefaultHeaders("Mercy", keepAlive: true)
                .UseDefaultFile("index.html")
                .UseStaticFile(rootPath: root)
                .UseMvc()
                .UseNotFound(root, "404.html");

            var condition = new ConditionCollection()
                .InsertCondition(new DomainCondition("localhost"));

            server.Bind(when: condition, run: app);
            server.Start();

            Console.ReadLine();
        }
    }
}