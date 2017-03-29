using Mercy.Models.Abstract;
using Mercy.Models.Conditions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercy.Models
{
    public class AppCondition : Condition
    {
        public override bool SatisfyCurrentCondition(HttpContext context)
        {
            return true;
        }
    }
}
