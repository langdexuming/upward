/****************************************************************
*   作者：yinruimin 5339
*   创建时间：2018/4/27 15:08:10
*   描述说明：HTTP工具
*
* Copyright (c) 2018 yinruimin Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本人机密信息，未经本人书面同意禁止向第三方披露．　│
*│　版权所有：yinruimin　　　　　　　　　　　　      │
*└──────────────────────────────────┘
*****************************************************************/
using Reggie.WPF.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.WPF.Utilities.Utils.Http
{
    public class HttpUtil
    {
        private const string TAG = nameof(HttpUtil);
        /// <summary>
        /// 请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpMethodConstants"></param>
        /// <returns></returns>
        public static HttpResult Request(string url, string httpMethodConstants = HttpMethodConstants.Get, string postMessage = "", string contentType = "")
        {
            HttpResult result = new HttpResult();
            if (!string.IsNullOrEmpty(url))
            {
                Logger.Info(TAG, $"请求网址：{url}");
            }

            if (!string.IsNullOrEmpty(postMessage))
            {
                Logger.Info(TAG, $"请求内容：{postMessage}");
            }

            try
            {
                var httpWebRequest = CreateRequest(url, httpMethodConstants, postMessage, contentType);
                var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                var stream = httpWebResponse.GetResponseStream();
                var reader = new StreamReader(stream, Encoding.UTF8);
                var content = reader.ReadToEnd();

                if(!string.IsNullOrEmpty(contentType)&&contentType.Contains("text/xml;"))
                {
                    content = DealXmlContent(content);
                }
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
        private static HttpWebRequest CreateRequest(string url, string httpMethodConstants,string postMessage,string contentType)
        {
            var httpMehod = httpMethodConstants;
            var httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            httpWebRequest.Method = httpMehod;
            if (!string.IsNullOrEmpty(contentType)) httpWebRequest.ContentType = contentType;
            switch (httpMehod)
            {
                case HttpMethodConstants.Get:
                    break;
                case HttpMethodConstants.Post:
                    using (Stream requestStream = httpWebRequest.GetRequestStream())
                    {
                        byte[] paramBytes = Encoding.UTF8.GetBytes(postMessage);
                        requestStream.Write(paramBytes, 0, paramBytes.Length);
                    }
                    break;
                case HttpMethodConstants.Put:
                    using (Stream requestStream = httpWebRequest.GetRequestStream())
                    {
                        byte[] paramBytes = Encoding.UTF8.GetBytes(postMessage);
                        requestStream.Write(paramBytes, 0, paramBytes.Length);
                    }
                    break;
                case HttpMethodConstants.Delete:
                    break;
                default:
                    break;
            }

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

        /// <summary>
        /// 上传文件
        /// </summary>
        public static HttpResult UploadFile(string url,string fileName, byte[] postMessage, string contentType= "multipart/form-data;charset=utf-8;")
        {
            var httpMethodConstants = HttpMethodConstants.Post;
            HttpResult result = new HttpResult();
            if (!string.IsNullOrEmpty(url))
            {
                Logger.Info(TAG, $"请求网址：{url}");

            }

            //请求头部信息 
            StringBuilder sbHeader = new StringBuilder(
                string.Format("Content-Disposition:form-data;name=\"files\";filename=\"{0}\"\r\nContent-Type:application/octet-stream\r\n\r\n",
                fileName));
            //byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sbHeader.ToString());

            try
            {
                var httpWebRequest = CreateFileRequest(url, httpMethodConstants, postMessage, contentType, sbHeader.ToString());
                //httpWebRequest.Headers.Add("Content-Disposition", "form-data");
                //httpWebRequest.Headers.Add("name", "file");
                //httpWebRequest.Headers.Add("filename", $"{fileName}");
                //httpWebRequest.Headers.Add("Content-Type", "application/octet-stream");

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
        private static HttpWebRequest CreateFileRequest(string url, string httpMethodConstants, byte[] postMessage, string contentType,string headers)
        {
            var httpMehod = httpMethodConstants;
            var httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            httpWebRequest.Method = httpMehod;

            // 随机分隔线
            string boundary = DateTime.Now.Ticks.ToString("X");
            contentType += $"boundary={boundary};";

            if (!string.IsNullOrEmpty(contentType))
                httpWebRequest.ContentType = contentType;

            byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(headers.ToString());

            using (Stream requestStream = httpWebRequest.GetRequestStream())
            {
                byte[] paramBytes = postMessage;
                //分隔头
                requestStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
                //头部
                requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                //内容
                requestStream.Write(paramBytes, 0, paramBytes.Length);
                //分隔尾
                requestStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            }

            return httpWebRequest;
        }

        /// <summary>
        /// 处理Http响应后的Xml格式内容
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DealXmlContent(string str)
        {
            return str.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&")
            .Replace("&apos;", "'").Replace("&quot;", "\"").Replace("&#xD;", "\r").Replace("&#xA;", "\n");
        }
    }
}
