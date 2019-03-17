using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reggie.Blog.Models
{
    public class SwitchFlag
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "bit(1)")]
        public bool IsVaild { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }
    }
}