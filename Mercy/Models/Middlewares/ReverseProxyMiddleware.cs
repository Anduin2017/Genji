using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Mercy.Library;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Mercy.Models.Middlewares
{
    public class ReverseProxyMiddleware : Middleware, IMiddleware
    {
        public string ProxyPath { get; set; }
        public ReverseProxyMiddleware(string proxyPath)
        {
            this.ProxyPath = proxyPath;
        }

        private WebResponse _RightNowResult { get; set; }
        public static HTTPService http { get; set; } = new HTTPService();

        protected async override Task<bool> Excutable(HttpContext context)
        {
            string target = ProxyPath + context.Request.PathWithArguments;
            try
            {
                _RightNowResult = await http.Get(target);
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected async override Task Excute(HttpContext context)
        {
            foreach (string head in _RightNowResult.Headers)
            {
                if (head == "Transfer-Encoding")
                {
                    continue;
                }
                if (head == "Content-Length")
                {
                    continue;
                }
                context.Response.Headers[head] = _RightNowResult.Headers[head];
            }
            context.Response.ResponseCode = 200;
            context.Response.Message = "OK";

            int contentLength = Convert.ToInt32(_RightNowResult.Headers["Content-Length"]);
            var myResponseStream = _RightNowResult.GetResponseStream();
            context.Response.Body = new byte[contentLength];
            await myResponseStream.ReadAsync(context.Response.Body, 0, context.Response.Body.Length);
            //context.Response.Body = Encoding.GetEncoding("utf-8").GetBytes(await myStreamReader.ReadToEndAsync());
             //myStreamReader.Dispose();
            myResponseStream.Dispose();
        }

        protected override void Mix(HttpContext context)
        {
        }
    }
}
