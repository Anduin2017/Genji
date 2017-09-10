using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercy.Models.ActionResults
{
    public class StringResult : IActionResult
    {
        private string _Content { get; set; }
        public StringResult(string Content)
        {
            _Content = Content;
        }
        public short StatusCode => 200;
        public string ContentType => "text/html; charset=utf-8";
        public string Messsage => "OK";
        public byte[] Render => Encoding.GetEncoding("utf-8").GetBytes(_Content);

    }
}
