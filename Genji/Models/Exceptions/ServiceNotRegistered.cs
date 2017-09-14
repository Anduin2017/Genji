using System;
using System.Collections.Generic;
using System.Text;

namespace Genji.Model.Exceptions
{
    public class ServiceNotRegistered : Exception
    {
        public ServiceNotRegistered(string message) : base(message) { }
    }
}
