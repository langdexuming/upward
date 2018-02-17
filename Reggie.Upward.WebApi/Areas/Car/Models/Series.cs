using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reggie.Upward.WebApi.Areas.Car.Models
{
    public class Series
    {
        public int SeriesId { get; set; }

        [StringLength(50)]
        public string SeriesName { get; set; }

        public ICollection<Model> Models { get; set; }

        [JsonIgnore]
        public int BrandId { get; set; }
    }
}
