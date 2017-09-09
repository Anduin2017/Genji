using Mercy.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercy.Library
{
    public abstract class Controller
    {
        public HttpContext HttpContext { get; set; }
    }
}
