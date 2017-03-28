using Mercy.Library;
using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Mercy.Models.Workers
{
    public class HttpReporter : IHttpReporter
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
}
