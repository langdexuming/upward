using System;
using System.ComponentModel.DataAnnotations;

namespace Reggie.Blog.Models
{
    public class InformalEssay
    {
        [Key]
        public long Id { get; set; }

        public int EssayCategoryId { get; set; }

        [StringLength(200)]
        [Required]
        public string Title { get; set; }

        [Required]
        public string Message { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm")]
        public DateTime CreateDateTime { get; set; }

        public EssayCategory EssayCategoryItem { get; set; }
    }
}