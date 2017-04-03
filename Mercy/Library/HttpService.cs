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
        public async Task<string> Post(string Url, string postDataStr, string Decode = "utf-8")
        {
            var request = WebRequest.CreateHttp(Url.ToString());
            if (CC.Count == 0)
            {
                request.CookieContainer = new CookieContainer();
                CC = request.CookieContainer;
            }
            else
            {
                request.CookieContainer = CC;
            }
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            var myRequestStream = await request.GetRequestStreamAsync();
            var myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("GB2312"));
            await myStreamWriter.WriteAsync(postDataStr.ToString().Trim('?'));
            myStreamWriter.Dispose();
            var response = await request.GetResponseAsync();
            var myResponseStream = response.GetResponseStream();
            var myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding(Decode));
            string retString = await myStreamReader.ReadToEndAsync();
            myStreamReader.Dispose();
            myResponseStream.Dispose();
            return retString;
        }

        public async Task<string> Get(string Url, string Coding = "utf-8")
        {
            var request = WebRequest.CreateHttp(Url.ToString());
            if (CC.Count == 0)
            {
                request.CookieContainer = new CookieContainer();
                CC = request.CookieContainer;
            }
            else
            {
                request.CookieContainer = CC;
            }
            request.Method = "GET";
            request.ContentType = "text/html;charset=" + Coding;
            var response = await request.GetResponseAsync();
            var myResponseStream = response.GetResponseStream();
            var myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding(Coding));
            string retString = await myStreamReader.ReadToEndAsync();
            myStreamReader.Dispose();
            myResponseStream.Dispose();
            return retString;
        }
    }
}
