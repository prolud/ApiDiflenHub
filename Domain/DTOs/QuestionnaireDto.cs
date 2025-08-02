namespace Domain.DTOs
{
    public class AnswerVerifyIn
    {
        public int LessonId { get; set; }
        public int QuestionId { get; set; }
        public required string UnityName { get; set; }
        public List<int> AlternativeIds { get; set; } = [];
    }

    public class AnswerVerifyOut
    {
        public required int QuestionId { get; set; }
        public required int AlternativeId { get; set; }
        public bool IsCorrect { get; set; }
    }

    public class GetLastAnswersOut
    {
        public List<AnswerVerifyOut> Answers { get; set; } = [];
        public int CurrentPointsWeight { get; set; }
        public bool WasAllQuestionsCorrectlyAnswered { get; set; }
        public bool WasCertificateAlreadyIssued { get; set; }
    }
}