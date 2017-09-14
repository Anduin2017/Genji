using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Genji.Models.Abstract
{
    public interface IHttpReporter
    {
        Task Report(Response response, NetworkStream stream);
    }
}
