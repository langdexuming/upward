/****************************************************************
*   作者：yinruimin 5339
*   创建时间：2018/6/5 8:50:57
*   描述说明：
*
* Copyright (c) 2018 yinruimin Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本人机密信息，未经本人书面同意禁止向第三方披露．　│
*│　版权所有：yinruimin　　　　　　　　　　　　      │
*└──────────────────────────────────┘
*****************************************************************/
using System;
using System.Security.Cryptography;
using System.Text;

namespace Reggie.WPF.Utilities.Utils.Security
{
    /// <summary>
    /// 字符串安全工具
    /// </summary>
    public class StringSecurityUtil
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="source">加密源</param>
        /// <returns></returns>
        public static string Encrypt(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                throw new ArgumentNullException(nameof(source));
            }
            //创建MD5加密
            var md5 = MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(source));
            var stringBuilder = new StringBuilder();
            foreach (var b in data)
            {
                stringBuilder.Append(b.ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encryptedStr">经加密的字符串</param>
        /// <returns></returns>
        public static string Decrypt(string encryptedStr)
        {
            throw new NotImplementedException();
        }
    }
}
