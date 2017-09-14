using System;
using System.Collections.Generic;
using System.Text;

namespace Genji.Models.Abstract
{
    public interface IFilter
    {
        bool ShouldContinue(HttpContext context);
    }
}
