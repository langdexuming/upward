using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Modules.Car
{
    public class Series
    {
        public int SeriesId { get; set; }

        public string SeriesName { get; set; }

        public ICollection<Model> Models { get; set; }

        [JsonIgnore]
        public int BrandId { get; set; }
    }
}
