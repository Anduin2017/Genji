using Genji.Models.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Genji.Models.ActionResults
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

        public async Task<byte[]> Render()
        {
            return await Task.Run(() =>
                File.ReadAllBytes(ViewFilePath));
        }
    }
}
