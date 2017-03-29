using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercy.Models
{
    public abstract class Controller
    {
        protected IActionResult View()
        {
            return null;
        }
    }
}
