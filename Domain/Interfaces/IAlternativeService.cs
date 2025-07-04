namespace Domain.Interfaces
{
    public interface IAlternativeService
    {
        public Task<int?> GetCorrectAlternativeIdAsync(int questionId);
    }
}