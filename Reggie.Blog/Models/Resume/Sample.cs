using System;
using System.ComponentModel.DataAnnotations;

namespace Reggie.Blog.Models
{
    public class Sample
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(200)]
        [DataType(DataType.Url)]
        public string ViewUrl { get; set; }

        [MaxLength(200)]
        [DataType(DataType.Url)]
        public string SourceUrl { get; set; }

        [DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm")]
        public DateTime CreateDateTime { get; set; }

        [DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm")]
        public DateTime UpdateDateTime { get; set; }
    }
}