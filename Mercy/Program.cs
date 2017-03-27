using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MiSP
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Method.Wait();
        }

        public static async Task Method()
        {
            var listener = new TcpListener(IPAddress.Any, 12222);
            listener.Start();
            while (true)
            {
                var soc = await listener.AcceptTcpClientAsync();

                var stream = soc.GetStream();


                byte[] bytes = new byte[1024];
                int length = await stream.ReadAsync(bytes, 0, bytes.Length);
                string requestString = Encoding.UTF8.GetString(bytes, 0, length);

                Console.WriteLine(requestString);
                string result = "HTTP/1.1 200 Found\r\nContent-type: text/html; charset=utf-8\r\n\r\n<h1>Hello world!</h1>";
                var byt = Encoding.UTF8.GetBytes(result);
                await stream.WriteAsync(byt, 0, byt.Length);
                stream.Dispose();
            }
        }
    }
}