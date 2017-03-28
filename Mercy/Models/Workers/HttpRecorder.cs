using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mercy.Models.Workers
{
    public class HttpRecorder : IHttpRecorder
    {
        public async Task Record(HttpContext context)
        {
            await Task.Delay(0);
            Console.WriteLine($"[{context.Response.ResponseCode}] HTTP {context.Request.Method}: {context.Request.Path}");
        }
        public async Task RecordException(Exception e)
        {
            await Task.Delay(0);
            Console.WriteLine("Mercy server crashed: " + e.Message);
        }
    }
}
