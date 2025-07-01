using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("lessons")]
    public class Lesson
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }

        [Column("video_url")]
        public string? VideoUrl { get; set; }

        [Column("unity_id")]
        public int UnityId { get; set; }

        public ICollection<Question> Questions { get; set; } = [];
    }
}