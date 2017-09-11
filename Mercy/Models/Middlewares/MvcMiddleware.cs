using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Mercy.Library;
using Mercy.Service;

namespace Mercy.Models.Middlewares
{
    public class MvcMiddleware : Middleware, IMiddleware
    {
        private MethodInfo action = null;
        private Type controller = null;
        private ServiceGroup services = null;
        private string viewLocation = string.Empty;
        public MvcMiddleware(string viewLocation,ServiceGroup services)
        {
            this.services = services;
            this.viewLocation = viewLocation;
        }
        protected async override Task<bool> Excutable(HttpContext context)
        {
            await Task.Delay(0);
            var items = Assembly.GetEntryAssembly().GetTypes();
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
            await Task.Delay(0);
            var instance = this.services.GetService(controller) as Controller;
            instance.HttpContext = context;
            var args = action.GetParameters();
            object[] parameters = new object[args.Length];
            for (int i = 0; i < args.Length; i++)
            {

                //var requirement = args[i].ParameterType;
                //parameters[i] = GetService(requirement);
            }
            var result = action.Invoke(instance, null) as IActionResult;
            context.Response.ResponseCode = result.StatusCode;
            context.Response.Message = result.Messsage;
            context.Response.Headers.Add("Content-type", result.ContentType);
            context.Response.Body = result.Render;
            return;
        }
    }
}
