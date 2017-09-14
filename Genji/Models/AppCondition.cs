using Genji.Models.Abstract;
using Genji.Models.Conditions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genji.Models
{
    public class AppCondition : Condition
    {
        public override bool SatisfyCurrentCondition(HttpContext context)
        {
            return true;
        }
    }
}
