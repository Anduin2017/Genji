using System;
using System.Collections.Generic;
using System.Text;

namespace Mercy.Models
{
    public class Request
    {
        public string Method { get; set; }
        public string Path { get; set; }
        public string HttpVersion { get; set; }
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    }
}
