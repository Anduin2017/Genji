using Mercy.Models.Abstract;
using Mercy.Models.Middlewares;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercy.Models
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
