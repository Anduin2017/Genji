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
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{ToTimeString(DateTime.Now)} [{context.Response.ResponseCode}] HTTP {context.Request.Method}: {context.Request.PathWithArguments}");
            Console.ResetColor();
        }
        public void Print(string content)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{ToTimeString(DateTime.Now)} [???] HTTP ???: {content}");
            Console.ResetColor();
        }
        public void RecordException(Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{ToTimeString(DateTime.Now)} [???] HTTP ???: Mercy server crashed: " + e.Message);
            Console.WriteLine(e.StackTrace);
            Console.ResetColor();
        }

        public void RecordIncoming()
        {
            if (RecordingIncoming)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{ToTimeString(DateTime.Now)} [???] HTTP Incoming....");
                Console.ResetColor();
            }
        }
    }
}
