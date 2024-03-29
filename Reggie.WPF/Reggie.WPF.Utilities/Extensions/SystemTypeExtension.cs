﻿/****************************************************************
*   作者：yinruimin
*   CLR版本：4.0.30319.42000
*   创建时间：2018/10/13 13:30:02
*   修改时间：2018/10/13 13:30:02
*   描述说明：
*
*   修改历史：
*
* Copyright (c) 2018 yinruimin Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本人机密信息，未经本人书面同意禁止向第三方披露．　│
*│　版权所有：yinruimin　　　　　　　　　　　　      │
*└──────────────────────────────────┘
*****************************************************************/
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.WPF.Utilities.Extensions
{
    public static class SystemTypeExtension
    {
        private const string TAG = nameof(SystemTypeExtension);

        /// <summary>
        /// 将字节类型数组转成可读字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToString(this byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0) return string.Empty;
            var str = string.Empty;
            var lastIndex = bytes.Length - 1;
            for (int i = 0; i < bytes.Length; i++)
            {
                str += string.Format("{0:X2}", bytes[i]) + " ";
                if ((i % 8 == 7) || i == lastIndex)
                {
                    str = str.TrimEnd() + "\n";
                }
            }
            return str.TrimEnd(' ');
        }

        /// <summary>
        /// 将字节类型数组转成可读字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToString(this List<byte> bytes)
        {
            if (bytes == null || bytes.Count == 0) return string.Empty;
            var str = string.Empty;
            var lastIndex = bytes.Count - 1;
            for (int i = 0; i < bytes.Count; i++)
            {
                str += string.Format("{0:X2}", bytes[i]) + " ";
                if ((i % 8 == 7) || i == lastIndex)
                {
                    str = str.TrimEnd() + "\n";
                }
            }
            return str.TrimEnd(' ');
        }

        /// <summary>
        /// 将字节指令字符串，转为字节数组
        /// </summary>
        /// <returns></returns>
        public static byte[] ToByteArray(this string text,char separator)
        {
            if (string.IsNullOrEmpty(text)) return null;

            var array = text.Split(separator);
            var result = new byte[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                try
                {
                    result[i] = Convert.ToByte(array[i],16);
                }
                catch (Exception ex)
                {
                    Logger.Warn(TAG, $"{text}不符合转化成字节数组的要求");
                    Logger.Warn(TAG, ex);
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 将字节指令字符串，转为字节数组
        /// </summary>
        /// <returns></returns>
        public static byte[] ToByteArray(this string text)
        {
            if (string.IsNullOrEmpty(text)) return null;

            //如果是奇数位,前面补0
            if (text.Length % 2 == 1)
            {
                text.Insert(0,"0");
            }

            var result = new byte[text.Length / 2];
            for (int i = 0; i < text.Length; i++)
            {
                try
                {
                    result[i / 2] = Convert.ToByte(text.Substring(i, 2), 16);
                }
                catch (Exception ex)
                {
                    Logger.Warn(TAG, $"{text}不符合转化成字节数组的要求");
                    Logger.Warn(TAG, ex);
                    break;
                }
                i++;
            }
            return result;
        }

        /// <summary>
        /// 拷贝数组
        /// </summary>
        /// <returns></returns>
        public static byte[] Copy(this byte[] arrays)
        {
            if (arrays == null)
            {
                return null;
            }

            var result = new byte[arrays.Length];
            for (int i = 0; i < arrays.Length; i++)
            {
                result[i] = arrays[i];
            }

            return result;
        }
    }
}
