using Reggie.BasicBracket.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.BasicBracket.Utils.Http
{
    public class HttpUtil
    {
        private const string TAG = nameof(HttpUtil);

        private static HttpClient _client;

        static HttpUtil()
        {
            _client = new HttpClient();
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public static async Task<HttpResult> Request(string url, string httpMethodConstants = HttpMethodConstants.Get)
        {
            var httpMehod = new HttpMethod(httpMethodConstants);

            HttpResult result = new HttpResult();
            try
            {
                //可使用HttpClientHandler,CookieContainer 扩展
                HttpResponseMessage response = await _client.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                Logger.Info(TAG, content);

                result.StatusCode = response.StatusCode;
                result.Data = content;
            }
            catch (HttpRequestException e)
            {
                Logger.Error(TAG, "\nException Caught!");
                Logger.Error(TAG, "Message :{0} ", e.Message);
                result.Message = e.Message;
            }

            return result;
        }

        //public static async Task<HttpResult<T>> Request<T>(string url,string httpMethodConstants=HttpMethodConstants.Get)
        //{
        //    var httpMehod = new HttpMethod(httpMethodConstants);

        //    HttpResult<T> result;
        //    try
        //    {
        //        //可使用HttpClientHandler,CookieContainer 扩展
        //        HttpResponseMessage response = await _client.GetAsync(url);

        //        response.EnsureSuccessStatusCode();

        //        var content = await response.Content.ReadAsStringAsync();

        //        Logger.Info(TAG, content);

        //        //直接解析
        //        var data = JsonConvert.DeserializeObject<T>(content);
        //        result = new HttpResult<T>
        //        {
        //            StatusCode = response.StatusCode,
        //            Message = "",
        //            Data = data
        //        };
        //    }
        //    catch (HttpRequestException e)
        //    {
        //        Logger.Error(TAG,"\nException Caught!");
        //        Logger.Error(TAG,"Message :{0} ", e.Message);
        //        result = null;
        //    }

        //    return result;
        //}
    }
}
