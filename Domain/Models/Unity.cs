using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("unities")]
    public class Unity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;

        public ICollection<Lesson> Lessons { get; set; } = [];
    }
}