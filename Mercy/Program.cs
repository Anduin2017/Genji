using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Mercy
{
    public static class Library
    {
        public static async Task<string> ReadToEnd(this NetworkStream stream)
        {
            byte[] bytes = new byte[1024];
            int length = await stream.ReadAsync(bytes, 0, bytes.Length);
            return Encoding.UTF8.GetString(bytes, 0, length);
        }
        public static async Task WriteString(this NetworkStream stream, string content)
        {
            var byt = Encoding.UTF8.GetBytes(content);
            await stream.WriteAsync(byt, 0, byt.Length);
        }

        public static async Task WriteLine(this NetworkStream stream, string content)
        {
            await stream.WriteString(content + "\r\n");
        }
    }
    public class Request
    {
        public string Method { get; set; }
        public string Path { get; set; }
        public string HttpVersion { get; set; }
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    }
    public class Response
    {
        public string HttpVersion { get; set; } = "HTTP/1.1";
        public short ResponseCode { get; set; } = 200;
        public string Message { get; set; } = "Found";
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        public byte[] Body { get; set; }
    }
    public class HttpContext
    {
        public Request Request { get; set; }
        public Response Response { get; set; }
    }
    public class HttpBuilder
    {
        public async Task<Request> Build(NetworkStream stream)
        {
            var source = await stream.ReadToEnd();
            var lines = source.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            var firstRows = lines[0].Split(' ');

            var request = new Request
            {
                Method = firstRows[0],
                Path = firstRows[1].Split('?')[0],
                HttpVersion = firstRows[2]
            };
            for (int i = 1; i < lines.Length; i++)
            {
                if (lines[i].Contains(":"))
                {
                    var twoStrings = lines[i].Split(':');
                    request.Headers.Add(twoStrings[0], twoStrings[1].Trim());
                }
            }
            return request;
        }
    }

    public class HttpExcuter
    {
        public Middleware Middlewares { get; set; }
        public HttpExcuter(Middleware middlewares)
        {
            Middlewares = middlewares;
        }
        public async Task Excute(HttpContext context)
        {
            context.Response = new Response();
            Middlewares.Run(context);
        }
    }
    public class HttpReporter
    {
        public async Task Report(Response response, NetworkStream stream)
        {
            await stream.WriteLine($"{response.HttpVersion} {response.ResponseCode} {response.Message}");
            foreach (var header in response.Headers)
            {
                await stream.WriteLine($"{header.Key}: {header.Value}");
            }
            await stream.WriteLine(string.Empty);
            await stream.WriteAsync(response.Body, 0, response.Body.Length);
            stream.Dispose();
        }
    }

    public class HttpRecorder
    {
        public async Task Record(HttpContext context)
        {
            Console.WriteLine($"[{context.Response.ResponseCode}] HTTP {context.Request.Method}: {context.Request.Path}");
        }
    }

    public class MercyServer
    {
        public int Port { get; set; }
        public HttpBuilder Builder { get; set; }
        public HttpExcuter Excuter { get; set; }
        public HttpReporter Reporter { get; set; }
        public HttpRecorder Recorder { get; set; }
        public MercyServer(int port)
        {
            Port = port;
        }
        public async Task Start()
        {
            var listener = new TcpListener(IPAddress.Any, Port);
            listener.Start();
            while (true)
            {
                var tcp = await listener.AcceptTcpClientAsync();
                var stream = tcp.GetStream();
                Task.Run(async () =>
                {
                    try
                    {
                        var httpContext = new HttpContext();
                        httpContext.Request = await Builder.Build(stream);
                        await Excuter.Excute(httpContext);
                        await Reporter.Report(httpContext.Response, stream);
                        await Recorder.Record(httpContext);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Mercy server crashed: " + e.Message);
                    }

                }).GetAwaiter();
            }
        }
    }

    public static class Program
    {
        public static void Main(string[] args)
        {
            int port = 12222;
            var server = new MercyServer(port);

            string root = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "wwwroot";

            var middlewares = new MiddlewareContainer()
                .InsertMiddleware(new DefaultFileMiddleware())
                .InsertMiddleware(new StaticFileMiddleware(root))
                .InsertMiddleware(new MvcMiddleware())
                .InsertMiddleware(new NotFoundMiddleware());

            server.Builder = new HttpBuilder();
            server.Excuter = new HttpExcuter(middlewares);
            server.Reporter = new HttpReporter();
            server.Recorder = new HttpRecorder();

            Console.WriteLine($"Application started at http://localhost:{port}/");
            server.Start().Wait();
        }
    }
    public abstract class Middleware
    {
        public Middleware NextMiddleware { get; set; }
        public virtual bool Excutable(HttpContext context)
        {
            return false;
        }
        public virtual Middleware InsertMiddleware(Middleware newMiddleware)
        {
            var pointer = this;
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
                NextMiddleware.Run(context);
            }
        }
        public virtual void Excute(HttpContext context)
        {

        }

    }
    public class MiddlewareContainer : Middleware
    {
    }

    public class NotFoundMiddleware : Middleware
    {
        public override bool Excutable(HttpContext context)
        {
            return true;
        }
        public override void Excute(HttpContext context)
        {
            context.Response.ResponseCode = 404;
            context.Response.Message = "Not found";
            context.Response.Body = Encoding.GetEncoding("utf-8").GetBytes("<h1>Not found!</h1>");
            context.Response.Headers.Add("Content-type", " text/html; charset=utf-8");
        }
    }

    public class StaticFileMiddleware : Middleware
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

        public override void Excute(HttpContext context)
        {
            string contextPath = context.Request.Path.Replace('/', Path.DirectorySeparatorChar);
            string filePath = RootPath + contextPath;
            var fileExtension = Path.GetExtension(filePath).TrimStart('.');
            context.Response.ResponseCode = 200;
            context.Response.Message = "OK";
            context.Response.Headers.Add("Content-Type", MIME.MIMETypesDictionary[fileExtension]);
            context.Response.Body = File.ReadAllBytes(filePath);
        }
    }

    public class DefaultFileMiddleware : Middleware
    {

    }

    public class MvcMiddleware : Middleware
    {

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