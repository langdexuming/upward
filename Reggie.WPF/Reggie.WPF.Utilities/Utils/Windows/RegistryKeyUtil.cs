/****************************************************************
*   作者：yinruimin 5339
*   创建时间：2018/4/27 15:08:10
*   描述说明：注册表操作工具
*
* Copyright (c) 2018 yinruimin Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本人机密信息，未经本人书面同意禁止向第三方披露．　│
*│　版权所有：yinruimin　　　　　　　　　　　　      │
*└──────────────────────────────────┘
*****************************************************************/
using Reggie.WPF.Utilities.Extensions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.WPF.Utilities.Utils.Windows
{
    public class RegistryKeyUtil
    {
        private static string TAG=nameof(RegistryKeyUtil);

        private static string _subKeyName = "";

        public static void SetSubKeyName(string name)
        {
            _subKeyName = name;
        }

        /// <summary>
        /// 根据名称获取值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetValue(string name)
        {
            var value = string.Empty;
            try
            {
                RegistryKey software = Registry.CurrentUser.OpenSubKey(_subKeyName, false);
                value = software.GetValue(name).ToString();
            }
            catch (Exception ex)
            {
                Logger.Error(TAG, ex);
                throw ex;
            }

            return value;
        }

        /// <summary>
        /// 根据名称设置值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void SetValue(string name, string value)
        {
            try
            {
                RegistryKey software = Registry.CurrentUser.OpenSubKey(_subKeyName, true);
                software.SetValue(name, value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除注册表
        /// </summary>
        public static void DeleteRegistryKey(RegistryKey registryKey,string parentKeyName, string value,out string message)
        {
            message = string.Empty;
            try
            {
                RegistryKey reg = registryKey.OpenSubKey(parentKeyName);
                var array = reg.GetValueNames();
                for (int i = 0; i < array.Length; i++)
                {
                    ;
                }
                //for (int i = 0; i < software.; i++)
                //{

                //}
            }
            catch (Exception ex)
            {
                message=ex.Message;
                ;
                //throw ex;
            }

          //  throw new NotImplementedException();
        }

        /// <summary>
        /// Find an text in regedit keys
        /// </summary>
        /// <param name="MasterKey">Parent Key</param>
        /// <param name="key">Key name to find</param>
        /// <returns></returns>
        public static List<string> FindKey(RegistryKey MasterKey, string key)
        {
            List<string> lstKeysFound = new List<string>();

            FindKeyinRegistryKey(MasterKey, key, ref lstKeysFound);

            return lstKeysFound;
        }

        /// <summary>
        /// Recursive function to find keys and increment list.
        /// </summary>
        /// <param name="MasterKey">Parent Key</param>
        /// <param name="key">Key name to find</param>
        /// <param name="lstKeysFound">List to increment</param>
        private static void FindKeyinRegistryKey(RegistryKey MasterKey, string key, ref List<string> lstKeysFound)
        {
            string[] keys = MasterKey.GetSubKeyNames();
            foreach (string itemKey in keys)
            {
                try
                {
                    if (itemKey.Contains(key))
                    {
                        lstKeysFound.Add(MasterKey.Name + " - " + itemKey);
                        break;
                    }

                    FindKeyinRegistryKey(MasterKey.OpenSubKey(itemKey), key, ref lstKeysFound);
                }
                catch // Prevent access denied
                { }

            }
        }
    }
}
