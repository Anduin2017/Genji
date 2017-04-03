using System;
using System.Collections.Generic;
using System.Text;

namespace Mercy.Models
{
    public class Request
    {
        public string Method { get; set; }
        public string Path { get; set; }
        public string PathWithArguments { get; set; }
        public string HttpVersion { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> Arguments { get; set; } = new Dictionary<string, string>();
    }
}
