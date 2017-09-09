using System;
using System.Collections.Generic;
using System.Text;

namespace Mercy.Exceptions
{
    public class RequestTerminatedException : Exception
    {
        public RequestTerminatedException(string message) : base(message) { }
    }
}
