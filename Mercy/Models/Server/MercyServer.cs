using Mercy.Models.Abstract;
using Mercy.Models.Conditions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Mercy.Models.Server
{
    public class MercyServer
    {
        private int Port { get; set; }
        public IHttpBuilder Builder { get; set; }
        public IHttpReporter Reporter { get; set; }
        public IHttpRecorder Recorder { get; set; }

        public Dictionary<ICondition, Middleware> Conditions { get; set; } = new Dictionary<ICondition, Middleware>();

        public MercyServer()
        {
            Port = 9000;
        }

        public MercyServer UsePort(int port)
        {
            Port = port;
            return this;
        }

        public MercyServer Bind(ICondition when, Middleware run)
        {
            this.Conditions.Add(when, run);
            return this;
        }

        public MercyServer UseBuilder(IHttpBuilder Builder)
        {
            this.Builder = Builder;
            return this;
        }
        public MercyServer UseReporter(IHttpReporter Reporter)
        {
            this.Reporter = Reporter;
            return this;
        }
        public MercyServer UseRecorder(IHttpRecorder Recorder)
        {
            this.Recorder = Recorder;
            return this;
        }

        private async Task _Accept(TcpListener listener)
        {
            var tcp = await listener.AcceptTcpClientAsync();
            var stream = tcp.GetStream();
            var httpContext = new HttpContext()
            {
                Request = await Builder.Build(stream),
                Response = new Response()
            };
            try
            {
                foreach (var condition in Conditions)
                {
                    if (condition.Key.SatisfyAllConditions(httpContext))
                    {
                        condition.Value.Run(httpContext);
                        break;
                    }
                }
                await Reporter.Report(httpContext.Response, stream);
                await Recorder.Record(httpContext);
            }
            catch (Exception e)
            {
                stream.Dispose();
                await Recorder.RecordException(e);
            }
        }

        public void Start()
        {
            Task.Run(() =>
            {
                var listener = new TcpListener(IPAddress.Any, Port);
                listener.Start();
                Console.WriteLine($"Application started at http://localhost:{Port}/");
                while (true)
                {
                    _Accept(listener).GetAwaiter();
                }
            });
        }
    }
}
