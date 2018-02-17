using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.Utilities.Utils.Http
{
    [Serializable]
    public class HttpResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
    }

    [Serializable]
    public class HttpResult<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
