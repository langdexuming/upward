using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Reggie.Upward.WebApi.Models
{
    public class Series
    {
        public int SeriesId { get; set; }

        [MaxLength(50)]
        public string SeriesName { get; set; }

        public ICollection<Model> Models { get; set; }

        [JsonIgnore]
        public int BrandId { get; set; }
    }
}