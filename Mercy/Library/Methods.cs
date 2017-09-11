using System;
using System.Collections.Generic;
using System.Text;

namespace Mercy.Library
{
    public static class Methods
    {
        public static string GetControllerName(Type type)
        {
            return type.Name.Replace("Controller", "");
        }
    }
}
