/****************************************************************
*   作者：yinruimin
*   CLR版本：4.0.30319.42000
*   创建时间：2018/10/5 23:07:46
*   修改时间：2018/10/5 23:07:46
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
using Reggie.WPF.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.WPF.Utilities.Utils.App
{
    public class AppConfigurationUtil
    {
        private const string TAG = nameof(AppConfigurationUtil);

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValue(string key)
        {
            var value = string.Empty;
            try
            {
                value = System.Configuration.ConfigurationSettings.AppSettings[key];
            }
            catch (Exception ex)
            {
                Logger.Error(TAG, ex);
            }

            return value;
        }
    }
}
