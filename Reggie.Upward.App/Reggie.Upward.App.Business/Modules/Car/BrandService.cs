using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reggie.BasicBracket.Utils.Http;
using Reggie.BasicBracket.Extensions;
using Newtonsoft.Json;

namespace Reggie.Upward.App.Business.Modules.Car
{
    public class BrandService : IBrandService
    {
        private const string TAG = nameof(BrandService);

        public Tuple<bool, List<Brand>> BrandsTuple = new Tuple<bool, List<Brand>>(false,null);

        public async Task<List<Brand>> GetAll()
        {
            //如果已经获取，则直接返回已获取的
            if (BrandsTuple.Item1) return BrandsTuple.Item2;

            List<Brand> brands=null;

            try
            {
                var httpResult = await HttpUtil.Request(WebApiUrl.GetBrandsUrl);
                if (httpResult?.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    brands = JsonConvert.DeserializeObject<List<Brand>>(httpResult.Data);
                    BrandsTuple = new Tuple<bool, List<Brand>>(true, brands);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(TAG,ex);
            }

            return brands;
        }
    }
}
