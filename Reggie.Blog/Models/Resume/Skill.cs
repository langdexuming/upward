using System;
using System.ComponentModel.DataAnnotations;

namespace Reggie.Blog.Models
{
    public class Skill
    {
        public int Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

        [Range(1, 10)]
        public int Level { get; set; }

        [DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm")]
        public DateTime CreateDateTime { get; set; }

        [DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm")]
        public DateTime LastUpdateDateTime { get; set; }
    }
}