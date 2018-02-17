using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reggie.Upward.WebApi.Areas.Car.Models
{
    public class Model
    {
        public int ModelId { get; set; }

        [StringLength(200)]
        public string ModelName { get; set; }

        [JsonIgnore]
        public int SeriesId { get; set; }

        [JsonIgnore]
        public int BrandId { get; set; }
    }
}
