using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reggie.Upward.WebApi.Models.Car;

namespace Reggie.Upward.WebApi.Models
{
    public class Brand
    {
        public int BrandId { get; set; }

        [MaxLength(50)]
        public string BrandName { get; set; }

        public ICollection<Series> Serieses { get; set; }
    }
}