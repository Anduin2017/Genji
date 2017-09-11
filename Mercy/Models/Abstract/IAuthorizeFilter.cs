using System;
using System.Collections.Generic;
using System.Text;

namespace Mercy.Models.Abstract
{
    public interface IAuthorizeFilter
    {
        bool ShouldContinue(HttpContext context);
    }
}
