using System.ComponentModel.DataAnnotations;

namespace Reggie.Blog.Models
{
    public class EssayCategory
    {
        [Key]
        public int EssayCategoryId { get; set; }

        [MaxLength(20)]
        public string Title { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [MaxLength(200)]
        public string Remark { get; set; }
    }
}