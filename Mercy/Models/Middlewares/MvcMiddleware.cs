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
        private MethodInfo action = null;
        private Type controller = null;
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
            var constructor = controller.GetConstructors()[0];
            var args = constructor.GetParameters();
            object[] parameters = new object[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                var type = args[i].ParameterType;
                parameters[i] = Assembly.GetAssembly(type).CreateInstance(type.FullName, true);
            }
            var instance = Assembly.GetAssembly(controller).CreateInstance(controller.FullName, true, BindingFlags.Default, null, parameters, null, null) as Controller;
            instance.HttpContext = context;
            var result = action.Invoke(instance, null) as IActionResult;
            context.Response.ResponseCode = result.StatusCode;
            context.Response.Message = result.Messsage;
            context.Response.Headers.Add("Content-type", result.ContentType);
            context.Response.Body = result.Render;
            return;
        }
    }
}
