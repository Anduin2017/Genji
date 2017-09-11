using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Mercy.Models.Workers
{
    public class HttpRecorder : IHttpRecorder
    {
        public bool RecordingIncoming { get; set; }
        public string log { get; set; }
        private string cache { get; set; }
        public HttpRecorder(string logpath = null, bool recordIncoming = false)
        {
            RecordingIncoming = recordIncoming;
            var now = DateTime.Now;
            log = $"{logpath}Mercy-Server-Log-{now.ToString("MM-dd-yy")}.txt";
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
            PrintAndWrite($"{ToTimeString(DateTime.Now)} [{context.Response.ResponseCode}] HTTP {context.Request.Method}: {context.Request.PathWithArguments}");
        }
        public void Print(string content)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintAndWrite($"{ToTimeString(DateTime.Now)} [???] HTTP ???: {content}");
        }
        public void Warn(string content)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            PrintAndWrite($"{ToTimeString(DateTime.Now)} [???] HTTP ???: {content}");
        }
        public void RecordException(Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            PrintAndWrite($"{ToTimeString(DateTime.Now)} [???] HTTP ???: Mercy server crashed because of {e.ToString()}: " + e.Message);
            PrintAndWrite(e.StackTrace);
        }

        public void RecordIncoming()
        {
            if (RecordingIncoming)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                PrintAndWrite($"{ToTimeString(DateTime.Now)} [???] HTTP Incoming....");
            }
        }

        public void PrintAndWrite(string text)
        {
            Console.WriteLine(text);
            if (!string.IsNullOrEmpty(log))
            {
                cache += text + "\r\n";
                if (cache.Length > 10000)
                {
                    File.AppendAllText(log, cache);
                    cache = string.Empty;
                }
            }
        }
    }
}
