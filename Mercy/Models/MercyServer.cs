using Mercy.Models.Abstract;
using Mercy.Models.Conditions;
using Mercy.Models.Exceptions;
using Mercy.Models.Middlewares;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Mercy.Models
{
    public class MercyServer : IServer
    {
        private int Port { get; set; }
        public IHttpBuilder Builder { get; set; }
        public IHttpReporter Reporter { get; set; }
        public IHttpRecorder Recorder { get; set; }
        public Dictionary<ICondition, IMiddleware> Conditions { get; set; } = new Dictionary<ICondition, IMiddleware>();
        public MercyServer()
        {
            Port = 9000;
        }
        public IServer UsePort(int port = 9000)
        {
            Port = port;
            return this;
        }
        public IServer UseBuilder(IHttpBuilder Builder)
        {
            this.Builder = Builder;
            return this;
        }
        public IServer UseReporter(IHttpReporter Reporter)
        {
            this.Reporter = Reporter;
            return this;
        }
        public IServer UseRecorder(IHttpRecorder Recorder)
        {
            this.Recorder = Recorder;
            return this;
        }
        public IServer Bind(ICondition when, IMiddleware run)
        {
            this.Conditions.Add(when, run);
            return this;
        }

        public Task Start()
        {
            return Task.Run(Listening);
        }

        private async Task Listening()
        {
            while (true)
            {
                var listener = new TcpListener(IPAddress.Any, Port);
                try
                {

                    listener.Start();
                    Console.ResetColor();
                    Console.WriteLine($"Mercy server started at http://localhost:{Port}/");
                    while (true)
                    {
                        var tcp = await listener.AcceptTcpClientAsync();
                        var stream = tcp.GetStream();
                        Excute(stream).GetAwaiter();
                    }
                }
                catch (Exception e)
                {
                    Recorder.RecordException(e);
                    await Task.Delay(5000);
                }
            }
        }

        private async Task Excute(NetworkStream stream)
        {
            Recorder.RecordIncoming();
            try
            {
                await Calculate(stream);
            }
            catch (RequestTerminatedException)
            {
                Recorder.Print("A request was terminated!");
            }
            catch (Exception e)
            {
                stream.Dispose();
                Recorder.RecordException(e);
            }
        }

        private async Task Calculate(NetworkStream stream)
        {
            var httpContext = new HttpContext()
            {
                Request = await Builder.Build(stream),
                Response = new Response()
            };
            foreach (var condition in Conditions)
            {
                if (condition.Key.SatisfyAllConditions(httpContext))
                {
                    await condition.Value.Run(httpContext);
                    break;
                }
            }
            await Reporter.Report(httpContext.Response, stream);
            Recorder.Record(httpContext);
        }
    }
}
