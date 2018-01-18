using System.ComponentModel.DataAnnotations;

namespace Reggie.Blog.Models
{
    public class ContentFlag
    {
        public int Id { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(500)]
        public string Content { get; set; }

        [MaxLength(200)]
        public string Remark { get; set; }
    }
}