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
        public MvcMiddleware(string viewLocation, ServiceGroup services)
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
                if (IsController(item) && Methods.GetControllerName(item) == context.Request.ControllerName)
                {
                    controller = item;
                    var methods = item.GetMethods();
                    foreach (var method in methods)
                    {
                        if (method.Name == context.Request.ActionName)
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


        protected override void Mix(HttpContext context)
        {
        }

        protected async override Task Excute(HttpContext context)
        {
            await Task.Delay(0);
            var instance = this.services.GetService(controller) as Controller;
            instance.HttpContext = context;
            instance.ViewLocation = viewLocation;

            foreach (var attribute in action.GetCustomAttributes())
            {
                var art = attribute as Attribute;
                if (art is IAuthorizeFilter)
                {
                    if (!(art as IAuthorizeFilter).ShouldContinue(context))
                    {
                        await NextMiddleware.Run(context);
                        return;
                    }
                }
            }
            var parameters = InjectArgs(action, context);
            var result = action.Invoke(instance, parameters);
            IActionResult response = null;
            if (result is IActionResult)
            {
                response = result as IActionResult;
            }
            if (result is Task<IActionResult>)
            {
                response = await (result as Task<IActionResult>);
            }
            context.Response.ResponseCode = response.StatusCode;
            context.Response.Message = response.Messsage;
            context.Response.Headers.Add("Content-type", response.ContentType);
            context.Response.Body = response.Render;
            return;
        }

        protected object[] InjectArgs(MethodInfo action, HttpContext context)
        {
            var args = action.GetParameters();
            object[] parameters = new object[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                if (context.Request.Arguments.ContainsKey(args[i].Name))
                {
                    parameters[i] = context.Request.Arguments[args[i].Name];
                }
            }
            return parameters;
        }
    }
}
