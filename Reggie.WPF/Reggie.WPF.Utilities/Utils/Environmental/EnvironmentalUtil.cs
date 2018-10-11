/****************************************************************
*   作者：yinruimin 5339
*   创建时间：2018/6/14 10:46:30
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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.WPF.Utilities.Utils.Environmental
{
    public enum EnvironmentalDigit
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default,

        _32Bit = Default,

        _64Bit
    }
    public class EnvironmentalUtil
    {
        /// <summary>
        /// 获取运行环境位数
        /// </summary>
        /// <returns></returns>
        public static EnvironmentalDigit GetCurrentEnvironmentalDigit()
        {
            if (IntPtr.Size == 8)
            {
                return EnvironmentalDigit._64Bit;
            }
            else if (IntPtr.Size == 4)
            {
                return EnvironmentalDigit._32Bit;
            }

            return EnvironmentalDigit.Default;
        }

        /// <summary>
        /// 获取当前进程已使用的内存
        /// </summary>
        /// <returns></returns>
        public static long GetUsedMemory()
        {
            //获得当前工作进程
            Process proc = Process.GetCurrentProcess();

            long usedMemory = proc.PrivateMemorySize64;
            return usedMemory;
        }

        
    }
}
