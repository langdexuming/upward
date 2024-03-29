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

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime CreateDateTime { get; set; }

        public EssayCategory EssayCategoryItem { get; set; }
    }
}