using System;
using System.Collections.Generic;
using System.Text;

namespace Mercy.Models.Abstract
{
    public interface IFilter
    {
        bool ShouldContinue(HttpContext context);
    }
}
