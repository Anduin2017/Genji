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
        public HttpRecorder(bool recordIncoming = false)
        {
            RecordingIncoming = recordIncoming;
        }
        public string ToTimeString(DateTime t)
        {
            return t.TimeOfDay.ToString();
        }
        public void Record(HttpContext context)
        {
            if (context.Response.ResponseCode == 200)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            Console.WriteLine($"{ToTimeString(DateTime.Now)} [{context.Response.ResponseCode}] HTTP {context.Request.Method}: {context.Request.PathWithArguments}");
        }
        public void Print(string content)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{ToTimeString(DateTime.Now)} [???] HTTP ???: {content}");
        }
        public void RecordException(Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{ToTimeString(DateTime.Now)} [???] HTTP ???: Mercy server crashed because of {e.ToString()}: " +  e.Message);
            Console.WriteLine(e.StackTrace);
        }

        public void RecordIncoming()
        {
            if (RecordingIncoming)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{ToTimeString(DateTime.Now)} [???] HTTP Incoming....");
            }
        }
    }
}
