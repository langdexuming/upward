using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class InformalEssay
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(200)]
        [Required]
        public string Title { get; set; }

        [Required]
        public string Message { get; set; }

        [MaxLength(50)]
        public string UserName { get; set; }

        [DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm")]
        public DateTime CreateDateTime { get; set; }
    }
}