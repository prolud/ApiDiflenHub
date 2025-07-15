using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("answers")]
    public class Answer
    {
        public int Id { get; set; }

        [Column("alternative_id")]
        public int AlternativeId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("question_id")]
        public int QuestionId { get; set; }

        [Column("lesson_id")]
        public int LessonId { get; set; }

        [Column("is_correct")]
        public bool IsCorrect { get; set; }

        public DateTime Created { get; set; }
    }
}