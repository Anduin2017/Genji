using System;
using System.Collections.Generic;
using System.Text;

namespace Genji.Models.Abstract
{
    public interface ICondition
    {
        ICondition NextCondition { get; set; }
        bool SatisfyCurrentCondition(HttpContext context);
        bool SatisfyAllConditions(HttpContext context);
        ICondition InsertCondition(ICondition newCondition);
    }
}
