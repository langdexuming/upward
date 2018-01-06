using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Modules.Car
{
    public class Model
    {
        public int ModelId { get; set; }

        public string ModelName { get; set; }

        [JsonIgnore]
        public int SeriesId { get; set; }

        [JsonIgnore]
        public int BrandId { get; set; }
    }
}
