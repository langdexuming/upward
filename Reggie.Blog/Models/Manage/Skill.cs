using System;
using System.ComponentModel.DataAnnotations;

namespace Reggie.Blog.Models
{
    public class Skill
    {
        public int Id { get; set; }

        [StringLength(20)]
        [Required]
        public string Name { get; set; }

        [Range(1, 10)]
        public int Level { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime CreateDateTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime LastUpdateDateTime { get; set; }
    }
}