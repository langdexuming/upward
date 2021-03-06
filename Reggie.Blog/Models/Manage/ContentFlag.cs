using System.ComponentModel.DataAnnotations;

namespace Reggie.Blog.Models
{
    public class ContentFlag
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(500)]
        public string Content { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }
    }
}