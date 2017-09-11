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
        private List<MethodInfo> actionsMatches = new List<MethodInfo>();
        private MethodInfo actionToRun;
        private Type controller = null;
        private ServiceGroup Services = null;
        private string ViewRoot = string.Empty;
        private bool CheckPathCase = false;
        public MvcMiddleware(string viewroot, ServiceGroup services, bool checkPathCase = false)
        {
            this.Services = services;
            this.ViewRoot = viewroot;
            this.CheckPathCase = checkPathCase;
        }

        protected async override Task<bool> Excutable(HttpContext context)
        {
            await Task.Delay(0);
            if (string.IsNullOrEmpty(context.Request.ControllerName) || string.IsNullOrEmpty(context.Request.ActionName))
            {
                return false;
            }
            actionsMatches.Clear();
            foreach (var item in Assembly.GetEntryAssembly().GetTypes())
            {
                if (Methods.IsController(item) && Methods.GetControllerName(item).StringEquals(context.Request.ControllerName, CheckPathCase))
                {
                    controller = item;
                    foreach (var method in item.GetMethods())
                    {
                        if (method.Name.StringEquals(context.Request.ActionName, CheckPathCase))
                        {
                            actionsMatches.Add(method);
                        }
                    }
                }
            }
            return actionsMatches.Count > 0;
        }

        protected override void Mix(HttpContext context)
        {
        }

        protected bool TryRun(HttpContext context, MethodBase action)
        {
            foreach (var attribute in Methods.ConnectAllAttributes(controller, action))
            {
                if (attribute is IAuthorizeFilter)
                {
                    if (!(attribute as IAuthorizeFilter).ShouldContinue(context))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        protected async override Task Excute(HttpContext context)
        {
            var instance = this.Services.GetService(controller) as Controller;
            instance.HttpContext = context;
            instance.ViewLocation = ViewRoot;
            foreach (var action in actionsMatches)
            {
                if (TryRun(context, action))
                {
                    actionToRun = action;
                    break;
                }
            }
            var parameters = Methods.InjectArgs(actionToRun, context);
            var result = actionToRun.Invoke(instance, parameters);
            IActionResult response = result is IActionResult ? response = result as IActionResult : await (result as Task<IActionResult>);
            await Methods.RenderResponse(context.Response, response);
        }
    }
}
