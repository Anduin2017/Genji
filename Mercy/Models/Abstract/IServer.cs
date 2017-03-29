using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mercy.Models.Abstract
{
    public interface IServer
    {
        IHttpBuilder Builder { get; set; }
        IHttpReporter Reporter { get; set; }
        IHttpRecorder Recorder { get; set; }
        IServer UsePort(int port = 9000);
        IServer UseBuilder(IHttpBuilder Builder);
        IServer UseReporter(IHttpReporter Reporter);
        IServer UseRecorder(IHttpRecorder Recorder);
        IServer Bind(ICondition when, IMiddleware run);
        Task Start();
    }
}
