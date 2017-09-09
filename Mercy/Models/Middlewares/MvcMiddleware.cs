using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Mercy.Library;

namespace Mercy.Models.Middlewares
{
    public class MvcMiddleware : Middleware, IMiddleware
    {
        public MvcMiddleware()
        {

        }
        protected async override Task<bool> Excutable(HttpContext context)
        {
            await Task.Delay(0);
            var items = Assembly.GetEntryAssembly().GetTypes();
            foreach (var item in items)
            {
                if (IsController(item) && GetControllerName(item).ToLower() == context.Request.ControllerName)
                {
                    var methods = item.GetMethods();
                    foreach (var method in methods)
                    {
                        if (method.Name.ToLower() == context.Request.ActionName)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool IsController(Type type)
        {
            return
                type.Name.EndsWith("Controller") &&
                type.Namespace.EndsWith("Controllers") &&
                type.Name != "Controller" &&
                type.IsSubclassOf(typeof(Controller)) &&
                type.IsPublic;
        }
        private string GetControllerName(Type type)
        {
            return type.Name.Replace("Controller", "");
        }

        protected override void Mix(HttpContext context)
        {
        }

        protected async override Task Excute(HttpContext context)
        {
            //await Task.Delay(0);
            var items = Assembly.GetEntryAssembly().GetTypes();
            MethodInfo action = null;
            Type controller = null;
            foreach (var item in items)
            {
                if (IsController(item) && GetControllerName(item).ToLower() == context.Request.ControllerName)
                {
                    controller = item;
                    var methods = item.GetMethods();
                    foreach (var method in methods)
                    {
                        if (method.Name.ToLower() == context.Request.ActionName)
                        {
                            action = method;
                        }
                    }
                }
            }
            var instance = Assembly.GetAssembly(controller).CreateInstance(controller.FullName) as Controller;
            instance.HttpContext = context;
            var result = action.Invoke(instance, null) as string;
            context.Response.ResponseCode = 200;
            context.Response.Message = "Ok";
            context.Response.Headers.Add("Content-type", "text/html; charset=utf-8");
            context.Response.Body = Encoding.GetEncoding("utf-8").GetBytes(result);
            return;
        }
    }
}
