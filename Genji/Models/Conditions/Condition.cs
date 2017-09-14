using Genji.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genji.Models.Conditions
{
    public abstract class Condition : ICondition
    {
        public ICondition NextCondition { get; set; }

        public abstract bool SatisfyCurrentCondition(HttpContext context);

        public bool SatisfyAllConditions(HttpContext context)
        {
            if (SatisfyCurrentCondition(context))
            {
                return NextCondition?.SatisfyAllConditions(context) ?? true;
            }
            return false;
        }
        public ICondition InsertCondition(ICondition newCondition)
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
