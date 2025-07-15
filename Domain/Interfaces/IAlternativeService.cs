using Domain.Models;

namespace Domain.Interfaces
{
    public interface IAlternativeService
    {
        public Task<Alternative?> GetCorrectAlternativeAsync(int questionId);
    }
}