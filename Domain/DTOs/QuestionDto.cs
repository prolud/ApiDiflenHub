namespace Domain.DTOs
{
    public class QuestionDtoOut
    {
        public int Id { get; set; }
        public string Statement { get; set; } = string.Empty;

        public ICollection<AlternativeDtoOut> Alternatives { get; set; } = [];
    }
}