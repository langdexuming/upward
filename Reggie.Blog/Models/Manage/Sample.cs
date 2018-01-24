using System;
using System.ComponentModel.DataAnnotations;

namespace Reggie.Blog.Models
{
    public class Sample
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(500)]
        public string Description { get; set; }

        [StringLength(200)]
        [DataType(DataType.Url)]
        public string ViewUrl { get; set; }

        [StringLength(200)]
        [DataType(DataType.Url)]
        public string SourceUrl { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime CreateDateTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime LastUpdateDateTime { get; set; }
    }
}