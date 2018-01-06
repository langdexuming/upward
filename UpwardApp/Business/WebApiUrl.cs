using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    internal class WebApiUrl
    {
        private const string BaseUrl = "http://127.0.0.1:5001/api";

        /// <summary>
        /// 获取汽车品牌
        /// </summary>
        public const string GetBrandsUrl = BaseUrl + "/brand";
    }
}
