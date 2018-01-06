using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace UpwardApi.Models
{
    public class Model
    {
        public int ModelId { get; set; }

        [MaxLength(200)]
        public string ModelName { get; set; }

        [JsonIgnore]
        public int SeriesId { get; set; }

        [JsonIgnore]
        public int BrandId { get; set; }

    }
}