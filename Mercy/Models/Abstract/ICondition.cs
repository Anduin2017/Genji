using System;
using System.Collections.Generic;
using System.Text;

namespace Mercy.Models.Abstract
{
    public interface ICondition
    {
        ICondition NextCondition { get; set; }
        bool SatisfyCurrentCondition(HttpContext context);
        bool SatisfyAllConditions(HttpContext context);
    }
}
