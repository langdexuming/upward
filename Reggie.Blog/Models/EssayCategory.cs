using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reggie.Blog.Models
{
    public class EssayCategory
    {
        [Key]
        public int EssayCategoryId { get; set; }

        [StringLength(20)]
        public string Title { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }

        public ICollection<InformalEssay> InformalEssays { get; set; }
    }
}