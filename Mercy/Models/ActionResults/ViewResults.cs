using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Mercy.Models.ActionResults
{
    public class ViewResult : IActionResult
    {
        public ViewResult()
        {

        }
        public string ViewFilePath { get; set; }
        public object Model { get; set; }
        public short StatusCode => 200;

        public string Messsage => "OK";

        public string ContentType => "text/html; charset=utf-8";

        public byte[] Render => File.ReadAllBytes(ViewFilePath);
    }
}
