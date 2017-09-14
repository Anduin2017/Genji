using Genji.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Genji.Models.Middlewares
{
    public class RouteMiddleware : Middleware, IMiddleware
    {
        public string SourcePath { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public RouteMiddleware(string sourcePath, string controllerName, string actionName)
        {
            SourcePath = sourcePath;
            ControllerName = controllerName;
            ActionName = actionName;
        }

        protected override void Mix(HttpContext context)
        {
            if (SourcePath == context.Request.Path)
            {
                context.Request.ControllerName = ControllerName;
                context.Request.ActionName = ActionName;
            }
        }

        protected async override Task<bool> Excutable(HttpContext context)
        {
            await Task.Delay(0);
            return false;
        }

        protected async override Task Excute(HttpContext context)
        {
            await Task.Delay(0);
            throw new NotImplementedException();
        }
    }
}
