using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercy.Models.ActionResults
{
    public class ViewResults : IActionResult
    {
        public short StatusCode => throw new NotImplementedException();

        public string Messsage => throw new NotImplementedException();

        public string ContentType => throw new NotImplementedException();

        public byte[] Render => throw new NotImplementedException();
    }
}
