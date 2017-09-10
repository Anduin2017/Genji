using System;
using System.Collections.Generic;
using System.Text;

namespace Mercy.Models.Abstract
{
    public interface IActionResult
    {
        short StatusCode { get; }
        string Messsage { get; }
        string ContentType { get; }
        byte[] Render { get; }
    }
}
