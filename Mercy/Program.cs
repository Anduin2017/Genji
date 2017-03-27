using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
        public byte ResponseCode { get; set; } = 200;
        public string Message { get; set; } = "Found";
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        public string Body { get; set; }
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
                Path = firstRows[1],
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
        public async Task<Response> Excute(Request request)
        {
            //await Task.Delay(3000);
            var response = new Response();
            response.Body = $"<h1>{DateTime.Now}</h1>";
            response.Headers.Add("Content-type", " text/html; charset=utf-8");
            return response;
        }
    }
    public class HttpReporter
    {
        public async Task Report(Response response, NetworkStream stream)
        {
            await stream.WriteLine($"{response.HttpVersion} {response.ResponseCode} {response.Message}");
            foreach(var header in response.Headers)
            {
                await stream.WriteLine($"{header.Key}: {header.Value}");
            }
            await stream.WriteLine(string.Empty);
            await stream.WriteLine(response.Body);
            stream.Dispose();
        }
    }

    public class MercyServer
    {
        public int Port { get; set; }
        public HttpBuilder Builder { get; set; }
        public HttpExcuter Excuter { get; set; }
        public HttpReporter Reporter { get; set; }
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
                    var httpContext = new HttpContext();
                    httpContext.Request = await Builder.Build(stream);
                    httpContext.Response = await Excuter.Excute(httpContext.Request);
                    await Reporter.Report(httpContext.Response, stream);
                    Console.WriteLine($"HTTP {httpContext.Request.Method} [{httpContext.Response.ResponseCode}]: {httpContext.Request.Path}");

                }).GetAwaiter();
            }
        }
    }

    public static class Program
    {
        public static void Main(string[] args)
        {
            var server = new MercyServer(12222);
            server.Builder = new HttpBuilder();
            server.Excuter = new HttpExcuter();
            server.Reporter = new HttpReporter();
            Console.WriteLine("Application started at http://localhost:12222");
            server.Start().Wait();
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