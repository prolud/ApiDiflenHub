using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories
{
    public class QuestionRepository(AppDbContext context) : BaseRepository<Question>(context), IQuestionRepository { }
}