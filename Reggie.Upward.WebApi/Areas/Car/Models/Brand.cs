using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reggie.Upward.WebApi.Areas.Car.Models
{
    public class Brand
    {
        public int BrandId { get; set; }

        [StringLength(50)]
        public string BrandName { get; set; }

        public ICollection<Series> Serieses { get; set; }
    }
}
