namespace Domain.DTOs
{
    public class AnswerVerifyIn
    {
        public int QuestionId { get; set; }
        public int AlternativeId { get; set; }
    }

    public class AnswerVerifyOut
    {
        public required int QuestionId { get; set; }
        public required int AlternativeId { get; set; }
        public bool IsCorrect { get; set; }
    }

    public class GetAnswersIn
    {
        public int UserId { get; set; }
        public int QuestionId { get; set; }
    }
}