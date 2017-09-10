using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Mercy.Models.ActionResults
{
    public class JsonResult : IActionResult
    {
        private object obj { get; set; }
        public JsonResult(object Content)
        {
            obj = Content;
        }
        public short StatusCode => 200;
        public string Messsage => "OK";
        public string ContentType => "application/json; charset=utf-8";
        public byte[] Render => Encoding.GetEncoding("utf-8").GetBytes(JsonConvert.SerializeObject(obj));
    }
}
