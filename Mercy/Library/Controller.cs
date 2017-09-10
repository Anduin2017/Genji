using Mercy.Models;
using Mercy.Models.ActionResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercy.Library
{
    public abstract class Controller
    {
        public HttpContext HttpContext { get; set; }
        public StringResult String(string input)
        {
            return new StringResult(input);
        }
    }
}
