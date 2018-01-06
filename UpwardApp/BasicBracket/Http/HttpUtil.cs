using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BasicBracket.Http
{
    public class HttpUtil
    {
        public static async Task<string> Request(string url,string httpMethodConstants=HttpMethodConstants.Get)
        {
            var httpMehod = new HttpMethod(httpMethodConstants);

            HttpClient client = new HttpClient();
            string result = string.Empty;
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            client.Dispose();

            return result;
        }
    }
}
