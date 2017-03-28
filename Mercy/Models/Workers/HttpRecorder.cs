using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mercy.Models.Workers
{
    public class HttpRecorder : IHttpRecorder
    {
        public bool RecordingIncoming { get; set; }
        public HttpRecorder(bool recordingIncoming = false)
        {
            RecordingIncoming = recordingIncoming;
        }
        public string ToTimeString(DateTime t)
        {
            return t.TimeOfDay.ToString();
        }
        public void Record(HttpContext context)
        {
            Console.WriteLine($"{ToTimeString(DateTime.Now)} [{context.Response.ResponseCode}] HTTP {context.Request.Method}: {context.Request.Path}");
        }
        public void RecordException(Exception e)
        {
            Console.WriteLine("Mercy server crashed: " + e.Message);
        }

        public void RecordIncoming()
        {
            if (RecordingIncoming)
            {
                Console.WriteLine($"{ToTimeString(DateTime.Now)} [???] HTTP Incoming....");
            }
        }
    }
}
