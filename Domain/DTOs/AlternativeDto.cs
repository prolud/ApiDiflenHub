namespace Domain.DTOs
{
    public class AlternativeDtoOut
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}