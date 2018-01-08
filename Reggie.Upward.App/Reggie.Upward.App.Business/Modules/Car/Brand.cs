using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.Upward.App.Business.Modules.Car
{
    public class Brand
    {
        public int BrandId { get; set; }

        public string BrandName { get; set; }

        public ICollection<Series> Serieses { get; set; }
    }
}
