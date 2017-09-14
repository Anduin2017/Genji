using Genji.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Genji.Models.Middlewares
{
    public abstract class Middleware : IMiddleware
    {
        public IMiddleware NextMiddleware { get; set; }
        protected abstract Task<bool> Excutable(HttpContext context);
        protected abstract Task Excute(HttpContext context);
        protected abstract void Mix(HttpContext context);

        public IMiddleware InsertMiddleware(IMiddleware newMiddleware)
        {
            IMiddleware pointer = this;
            while (pointer.NextMiddleware != null)
            {
                pointer = pointer.NextMiddleware;
            }
            pointer.NextMiddleware = newMiddleware;
            return this;
        }

        public async Task Run(HttpContext context)
        {
            Mix(context);
            if (await Excutable(context))
            {
                await Excute(context);
            }
            else
            {
                await NextMiddleware?.Run(context);
            }
        }
    }
}
