using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("questions")]
    public class Question
    {
        public int Id { get; set; }
        public string Statement { get; set; } = string.Empty;

        [Column("lesson_id")]
        public int LessonId { get; set; }

        public ICollection<Alternative> Alternatives { get; set; } = [];
    }
}