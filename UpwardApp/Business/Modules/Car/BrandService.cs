using BasicBracket.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Modules.Car
{
    public class BrandService : IBrandService
    {
        public Tuple<bool, List<Brand>> BrandsTuple = new Tuple<bool, List<Brand>>(false,null);

        public async Task<List<Brand>> Get()
        {
            //如果已经获取，则直接返回已获取的
            if (BrandsTuple.Item1) return BrandsTuple.Item2;

            HttpResult<List<Brand>> httpResult;
            try
            {
                var result = await HttpUtil.Request(WebApiUrl.GetBrandsUrl);
                httpResult = JsonConvert.DeserializeObject<HttpResult<List<Brand>>>(result);
            }
            catch (Exception ex)
            {
                httpResult = null;
            }

            List<Brand> brands;

            if (httpResult!=null&& httpResult.StatusCode== HttpStatusCode.Ok)
            {
                brands = httpResult.Data;

                BrandsTuple = new Tuple<bool, List<Brand>>(true, brands);
            }
            else
            {
                brands = null;
            }

            return brands;
        }
    }
}
