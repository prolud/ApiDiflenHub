using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Alternative
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;

        [Column("is_correct")]
        public bool IsCorrect { get; set; }

        [Column("question_id")]
        public int QuestionId { get; set; }
    }
}