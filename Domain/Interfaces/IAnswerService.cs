using Domain.Models;

namespace Domain.Interfaces
{
    public interface IAnswerService
    {
        public Task InsertAnswerAsync(Answer answer);
    }
}