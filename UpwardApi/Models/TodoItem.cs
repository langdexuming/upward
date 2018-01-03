using System.ComponentModel.DataAnnotations;

namespace UpwardApi.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}