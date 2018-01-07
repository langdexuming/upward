using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class LeaveMessage
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(500)]
        [Required]
        public string Message { get; set; }
        [MaxLength(50)]
        [Required]
        public string UserName { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}