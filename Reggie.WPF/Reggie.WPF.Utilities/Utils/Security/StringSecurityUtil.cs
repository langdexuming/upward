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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Reggie.WPF.Utilities.Utils.Security
{
    /// <summary>
    /// 字符串安全工具
    /// </summary>
    public class StringSecurityUtil
    {
        #region "定义加密字串变量"
        private static readonly SymmetricAlgorithm mCSP = new DESCryptoServiceProvider();  //定义访问数据加密标准 (DES) 算法的加密服务提供程序 (CSP) 版本的包装对象,此类是SymmetricAlgorithm的派生类;  //声明对称算法变量
        private const string CIV = "Mi9l/+7Zujhy12se6Yjy111A";  //初始化向量
        private const string CKEY = "jkHuIy9D/9i="; //密钥（常量）
        #endregion
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="source">加密源</param>
        /// <returns></returns>
        public static string Encrypt(string source)
        {
            ICryptoTransform ct; //定义基本的加密转换运算
            MemoryStream ms; //定义内存流
            CryptoStream cs; //定义将内存流链接到加密转换的流
            byte[] byt;
            //CreateEncryptor创建(对称数据)加密对象
            ct = mCSP.CreateEncryptor(Convert.FromBase64String(CKEY), Convert.FromBase64String(CIV)); //用指定的密钥和初始化向量创建对称数据加密标准
            byt = Encoding.UTF8.GetBytes(source); //将Value字符转换为UTF-8编码的字节序列
            ms = new MemoryStream(); //创建内存流
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write); //将内存流链接到加密转换的流
            cs.Write(byt, 0, byt.Length); //写入内存流
            cs.FlushFinalBlock(); //将缓冲区中的数据写入内存流，并清除缓冲区
            cs.Close(); //释放内存流
            return Convert.ToBase64String(ms.ToArray()); //将内存流转写入字节数组并转换为string字符
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encryptedStr">经加密的字符串</param>
        /// <returns></returns>
        public static string Decrypt(string encryptedStr)
        {
            ICryptoTransform ct; //定义基本的加密转换运算
            MemoryStream ms; //定义内存流
            CryptoStream cs; //定义将数据流链接到加密转换的流
            byte[] byt;
            ct = mCSP.CreateDecryptor(Convert.FromBase64String(CKEY), Convert.FromBase64String(CIV)); //用指定的密钥和初始化向量创建对称数据解密标准
            byt = Convert.FromBase64String(encryptedStr); //将Value(Base 64)字符转换成字节数组
            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return Encoding.UTF8.GetString(ms.ToArray()); //将字节数组中的所有字符解码为一个字符串
        }

        private static bool CompareArrayValue(int[] a, int[] b)
        {
            int a_len = a.Length;
            int b_len = b.Length;
            if (a_len == 0 || b_len == 0 || a_len != b_len)
                return false;
            else
            {
                for (int i = 0; i < a_len; i++)
                {
                    if (a[i] != b[i])
                        return false;
                }
            }
            return true;
        }
    }
}
