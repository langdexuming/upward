using System;

namespace UpwardApi.Models
{
    public class CarItem
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public string Owner { get; set; }
        public DateTime RegisterDateTime { get; set; }
    }
}