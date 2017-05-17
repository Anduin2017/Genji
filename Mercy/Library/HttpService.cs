using Mercy.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mercy.Library
{
    public class HTTPService
    {
        public CookieContainer CC = new CookieContainer();
        public async Task<Response> Post(string Url, string postDataStr)
        {
            throw new NotImplementedException();
            //var request = WebRequest.CreateHttp(Url.ToString());
            //request.CookieContainer = CC;
            //request.Method = "POST";
            //var myRequestStream = await request.GetRequestStreamAsync();
            //var myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));
            //await myStreamWriter.WriteAsync(postDataStr);

            //var response = await request.GetResponseAsync();
            ////var myResponseStream = response.GetResponseStream();
            ////var myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            ////string retString = await myStreamReader.ReadToEndAsync();
            //myStreamWriter.Dispose();
            //return response;
        }

        public async Task<Response> Get(string Url, string Coding = "utf-8")
        {
            throw new NotImplementedException();
        }
    }
}
