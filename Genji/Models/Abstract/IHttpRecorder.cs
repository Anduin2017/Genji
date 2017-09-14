using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Genji.Models.Abstract
{
    public interface IHttpRecorder
    {
        void Record(HttpContext context);
        void RecordException(Exception e);
        void Print(string content);
        void RecordIncoming();
        void Warn(string content);
    }
}
