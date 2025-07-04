namespace Domain.DTOs
{
    public class AnswerVerifyIn
    {
        public int QuestionId { get; set; }
        public int AlternativeId { get; set; }
    }
    
    public class AnswerVerifyOut
    {
        public int QuestionId { get; set; }
        public bool IsCorrect { get; set; }
    }
}