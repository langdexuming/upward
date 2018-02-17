using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.Upward.App.Business
{
    internal class WebApiUrl
    {
        public const string BaseUrl = "http://localhost:5001";

        /// <summary>
        /// 获取汽车品牌
        /// </summary>
        public const string GetBrandsUrl = BaseUrl + "/api/Car/Brands";

        /// <summary>
        /// 获取平台账号
        /// </summary>
        public const string GetAccountsUrl = BaseUrl + "/api/PlatformAccount/Accounts";
    }
}
