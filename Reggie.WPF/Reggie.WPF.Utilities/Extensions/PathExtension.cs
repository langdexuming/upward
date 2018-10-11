/****************************************************************
*   作者：yinruimin 5339
*   创建时间：2018/8/12 13:57:13
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
using System.Text;
using System.Threading.Tasks;

namespace Reggie.WPF.Utilities.Extensions
{
    public static class PathExtension
    {
        /// <summary>
        /// 判断文件是否是图片
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsPicture(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }

            string strFilter = ".jpeg|.gif|.jpg|.png|.bmp|.pic|.tiff|.ico|.iff|.lbm|.mag|.mac|.mpt|.opt|";
            char[] separtor = { '|' };
            string[] tempFileds = StringSplit(strFilter, separtor);
            foreach (string str in tempFileds)
            {
                if (str.ToUpper() == fileName.Substring(fileName.LastIndexOf("."), fileName.Length - fileName.LastIndexOf(".")).ToUpper()) { return true; }
            }
            return false;
        }

        /// <summary>
        /// 通过字符串，分隔符返回string[]数组
        /// </summary>
        /// <param name="s"></param>
        /// <param name="separtor"></param>
        /// <returns></returns>
        private static string[] StringSplit(string s, char[] separtor)
        {
            string[] tempFileds = s.Trim().Split(separtor); return tempFileds;
        }
    }
}
