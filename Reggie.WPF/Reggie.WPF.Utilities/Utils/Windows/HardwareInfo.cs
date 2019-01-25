/****************************************************************
*   作者：yinruimin
*   CLR版本：4.0.30319.42000
*   创建时间：2018/12/11 23:04:20
*   修改时间：2018/12/11 23:04:20
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
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.WPF.Utilities.Utils.Windows
{
    public class HardwareInfo
    {
        /// <summary>
        /// 取机器名 
        /// </summary>
        /// <returns></returns>
        public static string GetHostName()
        {
            return System.Net.Dns.GetHostName();
        }

        /// <summary>
        /// 取CPU编号
        /// </summary>
        /// <returns></returns>
        public static string GetCpuID()
        {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();

                string strCpuID = null;
                foreach (ManagementObject mo in moc)
                {
                    strCpuID = mo.Properties["ProcessorId"].Value.ToString();
                    break;
                }
                return strCpuID;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 取硬盘编号
        /// </summary>
        /// <returns></returns>
        public static string GetPhysicalMediaID()
        {
            var result = string.Empty;
            try
            {
                //创建ManagementObjectSearcher对象
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");

                //调用ManagementObjectSearcher类的Get方法取得硬盘序列号
                foreach (ManagementObject mo in searcher.Get())
                {
                    result = mo["SerialNumber"].ToString().Trim();//记录获得的磁盘序列号
                    break;
                }
            }
            catch
            {
                return "";
            }
            return result;
        }
    }
}
