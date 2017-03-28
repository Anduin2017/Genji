using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Mercy.Library;
using Mercy.Models;
using Mercy.Models.Abstract;
using Mercy.Models.Workers;
using Mercy.Models.Server;
using Mercy.Models.Conditions;
using Mercy.Models.Middlewares;

namespace Mercy
{

    public static class Program
    {
        public static void Main(string[] args)
        {
            string root = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "wwwroot";

            var app = new App()
                .InsertMiddleware(new ServerNameMiddleware("Mercy"))
                .InsertMiddleware(new DefaultFileMiddleware(defaultFileName: "index.html"))
                .InsertMiddleware(new StaticFileMiddleware(root))
                .InsertMiddleware(new MvcMiddleware())
                .InsertMiddleware(new NotFoundMiddleware());

            var condition = new ConditionCollection()
                .InsertCondition(new DomainCondition("localhost"));

            var server = new MercyServer()
                .UsePort(7000)
                .UseBuilder(new HttpBuilder())
                .UseReporter(new HttpReporter())
                .UseRecorder(new HttpRecorder())
                .Bind(when: condition, run: app);

            server.Start();
            Console.ReadLine();
        }
    }

    public class MvcMiddleware : Middleware, IMiddleware
    {
        protected override bool Excutable(HttpContext context)
        {
            return false;
        }

        protected override void Mix(HttpContext context)
        {
        }

        protected override void Excute(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }



    //public static class Program
    //{


    //    static void Main(string[] args)
    //    {
    //        Method().Wait();
    //    }

    //    public static async Task Method()
    //    {
    //        var listener = new TcpListener(IPAddress.Any, 12222);
    //        listener.Start();
    //        while (true)
    //        {
    //            Excute(await listener.AcceptTcpClientAsync()).GetAwaiter();
    //        }
    //    }

    //    public static async Task Excute(TcpClient soc)
    //    {
    //        var stream = soc.GetStream();
    //        string requestString = await stream.ReadToEnd();
    //        Console.WriteLine(requestString);
    //        await Task.Delay(2000);

    //        await stream.WriteLine("HTTP/1.1 200 Found");
    //        await stream.WriteLine("Content-type: text/html; charset=utf-8");
    //        await stream.WriteLine("Server: Mercy Server");
    //        await stream.WriteLine("");
    //        await stream.WriteLine($"<h1>Welcome to mercy server!</h1><p>{DateTime.Now}</p>");
    //        stream.Dispose();
    //    }
    //}
}