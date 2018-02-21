using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.Upward.App.Constants
{
    public class AppConstants
    {
        #region enums

        #endregion

        #region string

        #endregion

        #region methods
        /// <summary>
        /// 获取下载目录
        /// </summary>
        /// <returns></returns>
        public static string GetDownloadDirectory()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Download");
        }
        #endregion
    }
}
