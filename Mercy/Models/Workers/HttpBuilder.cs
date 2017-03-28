using Mercy.Library;
using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Mercy.Models.Workers
{
    public class HttpBuilder : IHttpBuilder
    {
        public async Task<Request> Build(NetworkStream stream)
        {
            var source = await stream.ReadToEnd();
            var lines = source.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            var firstRows = lines[0].Split(' ');

            var request = new Request
            {
                Method = firstRows[0],
                Path = firstRows[1].Split('?')[0],
                HttpVersion = firstRows[2]
            };
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
