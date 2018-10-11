using System;
using System.Net;

namespace Reggie.WPF.Utilities.Utils.Http
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
