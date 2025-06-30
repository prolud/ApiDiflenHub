using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? VideoUrl { get; set; }

        [Column("unity_id")]
        public int UnityId { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}