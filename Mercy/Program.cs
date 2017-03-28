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

namespace Mercy
{

    public static class Program
    {
        public static void Main(string[] args)
        {
            string root = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "wwwroot";

            var app = new App()
                .InsertMiddleware(new MercyServerHeaderMiddleware())
                .InsertMiddleware(new DefaultFileMiddleware())
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

    public interface IMiddleware
    {
        IMiddleware NextMiddleware { get; set; }
        bool Excutable(HttpContext context);
        void Run(HttpContext context);
    }

    public abstract class Middleware : IMiddleware
    {
        public IMiddleware NextMiddleware { get; set; }
        public abstract bool Excutable(HttpContext context);
        protected abstract void Excute(HttpContext context);

        public Middleware InsertMiddleware(IMiddleware newMiddleware)
        {
            IMiddleware pointer = this;
            while (pointer.NextMiddleware != null)
            {
                pointer = pointer.NextMiddleware;
            }
            pointer.NextMiddleware = newMiddleware;
            return this;
        }
        public void Run(HttpContext context)
        {
            if (Excutable(context))
            {
                Excute(context);
            }
            else
            {
                NextMiddleware?.Run(context);
            }
        }
    }

    public class App : Middleware, IMiddleware
    {
        public override bool Excutable(HttpContext context)
        {
            return false;
        }

        protected override void Excute(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }

    public class NotFoundMiddleware : Middleware, IMiddleware
    {
        public override bool Excutable(HttpContext context)
        {
            return true;
        }
        protected override void Excute(HttpContext context)
        {
            context.Response.ResponseCode = 404;
            context.Response.Message = "Not found";
            context.Response.Body = Encoding.GetEncoding("utf-8").GetBytes("<h1>Not found!</h1>");
            context.Response.Headers.Add("Content-type", " text/html; charset=utf-8");
        }
    }

    public class StaticFileMiddleware : Middleware, IMiddleware
    {
        public string RootPath { get; set; }
        public StaticFileMiddleware(string rootPath)
        {
            RootPath = rootPath;
        }

        public override bool Excutable(HttpContext context)
        {
            string contextPath = context.Request.Path.Replace('/', Path.DirectorySeparatorChar);
            string filePath = RootPath + contextPath;
            return File.Exists(filePath);
        }

        protected override void Excute(HttpContext context)
        {
            string contextPath = context.Request.Path.Replace('/', Path.DirectorySeparatorChar);
            string filePath = RootPath + contextPath;
            var fileExtension = Path.GetExtension(filePath).TrimStart('.');
            context.Response.ResponseCode = 200;
            context.Response.Message = "OK";
            context.Response.Headers.Add("cache-control", "max-age=3600");
            context.Response.Headers.Add("Content-Type", MIME.MIMETypesDictionary[fileExtension]);
            context.Response.Body = File.ReadAllBytes(filePath);
        }
    }

    public class MercyServerHeaderMiddleware : Middleware, IMiddleware
    {
        public override bool Excutable(HttpContext context)
        {
            return true;
        }

        protected override void Excute(HttpContext context)
        {
            context.Response.Headers.Add("Server", "Mercy");

            NextMiddleware?.Run(context);
        }
    }

    public class DefaultFileMiddleware : Middleware, IMiddleware
    {
        public override bool Excutable(HttpContext context)
        {
            return false;
        }

        protected override void Excute(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }

    public class MvcMiddleware : Middleware, IMiddleware
    {
        public override bool Excutable(HttpContext context)
        {
            return false;
        }

        protected override void Excute(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }


    public class MIME
    {
        public static readonly Dictionary<string, string> MIMETypesDictionary = new Dictionary<string, string>
        {
            {"avi", "video/x-msvideo"},
            {"apk","application/vnd.android.package-archive"},
            {"bmp", "image/bmp"},
            {"css", "text/css"},
            {"dll", "application/octet-stream"},
            {"doc", "application/msword"},
            {"docx","application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
            {"gif", "image/gif"},
            {"htm", "text/html"},
            {"html", "text/html"},
            {"ico", "image/x-icon"},
            {"jpeg", "image/jpeg"},
            {"jpg", "image/jpeg"},
            {"js", "application/x-javascript"},
            {"m4a", "audio/mp4a-latm"},
            {"mid", "audio/midi"},
            {"mov", "video/quicktime"},
            {"mp3", "audio/mpeg"},
            {"mp4", "video/mp4"},
            {"mpeg", "video/mpeg"},
            {"mpg", "video/mpeg"},
            {"ogg", "application/ogg"},
            {"pdf", "application/pdf"},
            {"png", "image/png"},
            {"ppt", "application/vnd.ms-powerpoint"},
            {"pptx","application/vnd.openxmlformats-officedocument.presentationml.presentation"},
            {"ppsx","application/vnd.openxmlformats-officedocument.presentationml.slideshow"},
            {"swf", "application/x-shockwave-flash"},
            {"svg", "image/svg+xml"},
            {"tif", "image/tiff"},
            {"txt", "text/plain"},
            {"xhtml", "application/xhtml+xml"},
            {"xls", "application/vnd.ms-excel"},
            {"xlsx","application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
            {"zip", "application/zip"},
            {"iso", "application/iso" },
            {"7z","application/x-7z-compressed" },
            {"rtf", "text/rtf"},
            {"m4u", "video/vnd.mpegurl"},
            {"tiff", "image/tiff"},
            {"woff","application/x-font-woff"},
            {"woff2","application/x-font-woff2"},
            {"ttf","application/x-font-truetype"},
            {"otf","application/x-font-opentype"},
            {"eot","application/application/vnd.ms-fontobject"}
        };
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