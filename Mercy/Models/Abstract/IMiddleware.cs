using System;
using System.Collections.Generic;
using System.Text;

namespace Mercy.Models.Abstract
{
    public interface IMiddleware
    {
        IMiddleware NextMiddleware { get; set; }
        void Run(HttpContext context);
        IMiddleware InsertMiddleware(IMiddleware newMiddleware);
    }
}
