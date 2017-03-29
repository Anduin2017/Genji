using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercy.Models.Conditions
{
    public class DomainCondition : Condition, ICondition
    {
        public string Domain { get; set; }

        public DomainCondition(string domain)
        {
            Domain = domain;
        }
        public override bool SatisfyCurrentCondition(HttpContext context)
        {
            var runningDomain = context.Request.Headers["Host"].Trim();
            if (Domain == "*")
            {
                return true;
            }
            return Domain == runningDomain;
        }
    }
}
