using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercy.Models.Conditions
{
    public class ConditionCollection : ICondition
    {
        public ICondition NextCondition { get; set; }
        public virtual bool SatisfyCurrentCondition(HttpContext context)
        {
            return true;
        }
        public bool SatisfyAllConditions(HttpContext context)
        {
            if (SatisfyCurrentCondition(context))
            {
                return NextCondition?.SatisfyAllConditions(context) ?? true;
            }
            return false;
        }
        public virtual ConditionCollection InsertCondition(ICondition newCondition)
        {
            ICondition pointer = this;
            while (pointer.NextCondition != null)
            {
                pointer = pointer.NextCondition;
            }
            pointer.NextCondition = newCondition;
            return this;
        }
    }
}
