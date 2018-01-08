using System;
using Newtonsoft.Json;

namespace Reggie.Upward.WebApi.BasicBracket
{
    [Serializable]
    public class HttpResult
    {

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data
        { get; set; }

        public HttpResult(HttpStatusCode statusCode, string message)
        {
            StatusCode = (int)statusCode;
            Message = message;
            Data = null;
        }

        public HttpResult(object data)
        {
            StatusCode = (int)HttpStatusCode.Ok;
            Message = "OK";
            Data = data;
        }

        [JsonIgnore]
        public static readonly HttpResult NoFound = new HttpResult(HttpStatusCode.Ok, "Not Found !");
        [JsonIgnore]
        public static readonly HttpResult BadRequest = new HttpResult(HttpStatusCode.BadRequest, "BadRequest !");
    }
}