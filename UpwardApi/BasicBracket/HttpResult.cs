using System;
using Newtonsoft.Json;

namespace UpwardApi.BasicBracket
{
    [Serializable]
    public class HttpResult
    {

        public string StatusCode { get; set; }
        public string Message { get; set; }
        public object Data
        { get; set; }

        public HttpResult(string statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
            Data = null;
        }

        public HttpResult(object data)
        {
            StatusCode = HttpStatusCode.Ok;
            Message = "OK";
            Data = data;
        }

        [JsonIgnore]
        public static readonly HttpResult NoFound = new HttpResult(HttpStatusCode.Ok, "Not Found !");
        [JsonIgnore]
        public static readonly HttpResult BadRequest = new HttpResult(HttpStatusCode.BadRequest, "BadRequest !");
    }
}