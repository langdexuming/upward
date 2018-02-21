using Reggie.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.Utilities.Utils.Http
{
    public class HttpUtil
    {
        private const string TAG = nameof(HttpUtil);

        private static bool _isExistBearerToken;

#if NET4_0
        /// <summary>
        /// 请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpMethodConstants"></param>
        /// <returns></returns>
        public static HttpResult Request(string url, string httpMethodConstants = HttpMethodConstants.Get)
        {
            HttpResult result = new HttpResult();
            try
            {
                var httpWebRequest = CreateRequest(url, httpMethodConstants);
                var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                var stream = httpWebResponse.GetResponseStream();
                var reader = new StreamReader(stream, Encoding.UTF8);
                var content = reader.ReadToEnd();

                Logger.Info(TAG, content);

                result.StatusCode = httpWebResponse.StatusCode;
                result.Message = httpWebResponse.StatusDescription;
                result.Data = content;

                stream.Close();
            }
            catch (Exception ex)
            {
                Logger.Error(TAG, "\nException Caught!");
                Logger.Error(TAG, "Message :{0} ", ex.Message);
                result = null;
            }
            finally
            {

            }

            return result;
        }

        /// <summary>
        /// 创建请求
        /// </summary>
        private static HttpWebRequest CreateRequest(string url, string httpMethodConstants)
        {
            var httpMehod = httpMethodConstants;

            var httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            httpWebRequest.Method = httpMehod;

            return httpWebRequest;
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        public static byte[] DownloadFile(string url)
        {
            byte[] result = null;
            try
            {
                var httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                long totalBytes = httpWebResponse.ContentLength;
                result = new byte[totalBytes];

                var stream = httpWebResponse.GetResponseStream();
                long totalDownloadedByte = 0;
                byte[] buffer = new byte[1024];
                int osize = stream.Read(buffer, 0, (int)buffer.Length);
                while (osize > 0)
                {
                    if (osize < 1024)
                    {
                        buffer.Take(osize).ToArray().CopyTo(result, totalDownloadedByte);
                    }
                    else
                    {
                        buffer.CopyTo(result, totalDownloadedByte);
                    }

                    totalDownloadedByte = osize + totalDownloadedByte;
                    osize = stream.Read(buffer, 0, (int)buffer.Length);
                }

                stream.Close();
            }
            catch (Exception ex)
            {
                Logger.Error(TAG, "\nException Caught!");
                Logger.Error(TAG, "Message :{0} ", ex.Message);
            }

            return result;
        }
#else
        private static HttpClient _client;

        static HttpUtil()
        {
            _client = new HttpClient();
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        /// <summary>
        /// discover endpoints from metadata and setHeader
        /// </summary>
        public static async Task SetBearerToken(string baseUrl,string cilentId, string secret, string apiName)
        {
            if (_isExistBearerToken) return;

            var disco = await DiscoveryClient.GetAsync(baseUrl);
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, cilentId, secret,AuthenticationStyle.BasicAuthentication);
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync(apiName);

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            _client.SetBearerToken(tokenResponse.AccessToken);
            _isExistBearerToken = true;
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

        /// <summary>
        /// 下载文件
        /// </summary>
        public static async Task<byte[]> DownloadFile(string url)
        {
            byte[] result = null;
            try
            {
                //可使用HttpClientHandler,CookieContainer 扩展
                HttpResponseMessage response = await _client.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsByteArrayAsync();

                Logger.Info(TAG, content);

                result = content;
            }
            catch (HttpRequestException e)
            {
                Logger.Error(TAG, "\nException Caught!");
                Logger.Error(TAG, "Message :{0} ", e.Message);
            }

            return result;
        }
#endif
    }
}
