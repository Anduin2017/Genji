using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mercy.Models.Abstract
{
    public interface IMiddleware
    {
        IMiddleware NextMiddleware { get; set; }
        Task Run(HttpContext context);
        IMiddleware InsertMiddleware(IMiddleware newMiddleware);
    }
}
