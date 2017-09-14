using Genji.Library;
using Genji.Models.Abstract;
using Genji.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Genji.Models.Workers
{
    public class HttpBuilder : IHttpBuilder
    {
        public async Task<Request> Build(NetworkStream stream)
        {
            var source = await stream.ReadToEnd();
            if (string.IsNullOrWhiteSpace(source))
            {
                throw new RequestTerminatedException("A request was terminated!");
            }
            var lines = source.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            var firstRows = lines[0].Split(' ');

            //Path and http basic
            var request = new Request
            {
                Method = firstRows[0],
                Path = firstRows[1].Split('?')[0],
                PathWithArguments = firstRows[1],
                HttpVersion = firstRows[2]
            };

            //Construct Controller name and action name
            var paths = request.Path.Split('/');
            if (paths.Length >= 3)
            {
                request.ControllerName = paths[1];
                request.ActionName = paths[2];
            }

            //Argumtnets
            var args = request.PathWithArguments.Split('&', '?');
            foreach (var arg in args)
            {
                var splitEqual = arg.Split('=');
                if (splitEqual.Length == 2)
                {
                    request.Arguments.Add(splitEqual[0], WebUtility.UrlDecode(splitEqual[1]));
                }
            }

            //headers
            for (int i = 1; i < lines.Length; i++)
            {
                if (lines[i].Contains(":"))
                {
                    var twoStrings = lines[i].Split(':');
                    request.Headers.Add(twoStrings[0], twoStrings[1].Trim());
                }
            }
            return request;
        }
    }
}
