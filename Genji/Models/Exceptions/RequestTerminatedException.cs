using System;
using System.Collections.Generic;
using System.Text;

namespace Genji.Models.Exceptions
{
    public class RequestTerminatedException : Exception
    {
        public RequestTerminatedException(string message) : base(message) { }
    }
}
