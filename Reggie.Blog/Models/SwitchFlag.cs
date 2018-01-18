using System.ComponentModel.DataAnnotations;

namespace Reggie.Blog.Models
{
    public class SwitchFlag
    {
        public int Id { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        public bool IsVaild { get; set; }

        [MaxLength(200)]
        public string Remark { get; set; }
    }
}