using System;
using System.ComponentModel.DataAnnotations;

namespace Reggie.Blog.Models
{
    public class Skill
    {
        public int Id { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        [Range(1, 10)]
        public int Level { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime LastUpdateDateTime { get; set; }
    }
}