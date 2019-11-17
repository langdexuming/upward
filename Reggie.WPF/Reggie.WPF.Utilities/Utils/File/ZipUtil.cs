/****************************************************************
*   作者：yinruimin 5339
*   创建时间：2019/2/27 15:35:42
*   描述说明：
*
* Copyright (c) 2019 GoSunCN Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：高新兴科技集团股份有限公司　　　　　　　　　　　　      │
*└──────────────────────────────────┘
*****************************************************************/
using Reggie.WPF.Utilities.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Reggie.WPF.Utilities.Utils.File
{
    public class ZipUtil
    {
        private const string TAG = nameof(ZipUtil);
        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="filePath"></param>
        public static void Decompress(string filePath, string targetPath)
        {
            try
            {
                ZipFile.ExtractToDirectory(filePath, targetPath);
            }
            catch (Exception ex)
            {
                Logger.Error(TAG, ex);
            }

        }
    }
}
