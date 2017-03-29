using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Mercy.Library
{
    public static class NetworkStreamExtend
    {
        public static async Task<string> ReadToEnd(this NetworkStream stream)
        {
            byte[] bytes = new byte[1024];
            int length = await stream.ReadAsync(bytes, 0, bytes.Length);
            return Encoding.UTF8.GetString(bytes, 0, length);
        }
        public static async Task WriteString(this NetworkStream stream, string content)
        {
            var byt = Encoding.UTF8.GetBytes(content);
            await stream.WriteAsync(byt, 0, byt.Length);
        }

        public static async Task WriteLine(this NetworkStream stream, string content)
        {
            await stream.WriteString(content + "\r\n");
        }
    }
}
