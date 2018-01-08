using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.BasicBracket.Http
{
    public class HttpUtil
    {
        private static HttpClient _client;

        static HttpUtil()
        {
            _client = new HttpClient();
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public static async Task<string> Request(string url,string httpMethodConstants=HttpMethodConstants.Get)
        {
            var httpMehod = new HttpMethod(httpMethodConstants);

            string result = string.Empty;
            try
            {
                //可使用HttpClientHandler,CookieContainer 扩展
                HttpResponseMessage response = await _client.GetAsync(url);

                response.EnsureSuccessStatusCode();
                result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return result;
        }
    }
}
