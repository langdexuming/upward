using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reggie.Upward.WebApi.Areas.Car.Models
{
    public class CarItem
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public string Owner { get; set; }
        public DateTime RegisterDateTime { get; set; }
    }
}
