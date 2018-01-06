using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UpwardApi.Models.Car;

namespace UpwardApi.Models
{
    public class Brand
    {
        public int BrandId { get; set; }

        [MaxLength(50)]
        public string BrandName { get; set; }

        public ICollection<Series> Serieses { get; set; }
    }
}