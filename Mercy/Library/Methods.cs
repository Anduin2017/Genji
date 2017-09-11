using Mercy.Models;
using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mercy.Library
{
    public static class Methods
    {
        public static string GetControllerName(Type type)
        {
            return type.Name.Replace("Controller", "");
        }

        public static bool StringEquals(this string source, string target, bool checkCase)
        {
            if (source == null || target==null)
            {
                return false;
            }
            if(checkCase)
            {
                return source.Equals(target);
            }
            else
            {
                return source.ToLower().Equals(target.ToLower());
            }
        }

        public static bool IsController(Type type)
        {
            return
                type.Name.EndsWith("Controller") &&
                type.Namespace.EndsWith("Controllers") &&
                type.Name != "Controller" &&
                type.IsSubclassOf(typeof(Controller)) &&
                type.IsPublic;
        }

        public static object[] InjectArgs(MethodInfo action, HttpContext context)
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

        public static IEnumerable<Attribute> ConnectAllAttributes(Type controller, MethodBase action)
        {
            List<Attribute> allattributes = new List<Attribute>();
            foreach (var attribute in controller.GetCustomAttributes())
                allattributes.Add(attribute);
            foreach (var attribute in action.GetCustomAttributes())
                allattributes.Add(attribute);
            return allattributes;
        }

        public static async Task RenderResponse(Response response, IActionResult actionResult)
        {
            response.ResponseCode = actionResult.StatusCode;
            response.Message = actionResult.Messsage;
            response.Headers.Add("Content-type", actionResult.ContentType);
            response.Body = await actionResult.Render();
        }
    }
}
