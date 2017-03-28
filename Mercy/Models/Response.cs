using System;
using System.Collections.Generic;
using System.Text;

namespace Mercy.Models
{
    public class Response
    {
        public string HttpVersion { get; set; } = "HTTP/1.1";
        public short ResponseCode { get; set; } = 500;
        public string Message { get; set; } = "Found";
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        public byte[] Body { get; set; } = new byte[0];
    }
}
