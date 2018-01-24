using System.ComponentModel.DataAnnotations;

namespace Reggie.Blog.Models
{
    public class SwitchFlag
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public bool IsVaild { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }
    }
}