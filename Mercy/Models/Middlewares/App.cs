using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercy.Models.Middlewares
{
    public class App : Middleware, IMiddleware
    {
        protected override void Mix(HttpContext context)
        {
        }
        protected override bool Excutable(HttpContext context)
        {
            return false;
        }

        protected override void Excute(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}
