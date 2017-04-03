using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

namespace Mercy.Models.Middlewares
{
    public class MvcMiddleware : Middleware, IMiddleware
    {
        public MvcMiddleware()
        {

        }
        protected async override Task<bool> Excutable(HttpContext context)
        {
            //var items = Assembly.GetEntryAssembly().GetTypes();
            //foreach (var item in items)
            //{
            //    if (IsController(item) && GetControllerName(item).ToLower() == context.Request.ControllerName)
            //    {
            //        var methods = item.GetMethods();
            //        foreach (var method in methods)
            //        {
            //            if (method.Name.ToLower() == context.Request.ActionName)
            //            {
            //                return true;
            //            }
            //        }
            //    }
            //}
            return false;
        }

        private bool IsController(Type type)
        {
            return
                type.Name.EndsWith("Controller") &&
                type.Namespace.EndsWith("Controllers") &&
                type.Name != "Controller";
        }
        private string GetControllerName(Type type)
        {
            return type.Name.Replace("Controller", "");
        }

        protected override void Mix(HttpContext context)
        {
        }

        protected override void Excute(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}
