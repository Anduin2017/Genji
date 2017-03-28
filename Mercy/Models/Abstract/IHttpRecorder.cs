using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mercy.Models.Abstract
{
    public interface IHttpRecorder
    {
        Task Record(HttpContext context);
        Task RecordException(Exception e);
        Task RecordIncoming();
    }
}
